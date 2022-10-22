using System;

namespace Nemag.Auxiliar.Encoding
{
    /*
	
    In pseudo-code: 
	
    // initialise //
    CRITICAL_VALUES := CR, LF, NUL, =;
    o := 1;
    // step through input file //
    FOR i := 1 TO END_OF_FILE

        // read character and add the 42 offset //
        temp := input[i];
        temp := (temp + 42) MOD 256;

        // check for critical value //
        IF temp ISIN CRITICAL_VALUES THEN 
            // critical value, so send '=' to output, followed by value + 64 //
            output[o] := '=';
            o := o + 1;
            output[o] := (temp + 64) MOD 256;

        // not a critical value, so copy input to output //
        ELSE output[o] := temp;

        o := o + 1; 
        // repeat until finished //
    NEXT i; 

    Note: the input is the original file. The output is the file encoded. 
    yDecoding
    March, 2002
    This is exactly like encoding, in reverse.

    In pseudo-code: 
    // initialise //
    o := 1;

    // step through input file //
    FOR i := 1 TO END_OF_FILE
        // read character //
        temp := input[i];

        // check for escape character //
        IF temp := '=' THEN 
            // read next character and subtract 64 //
            i := i + 1;
            temp := input[i];
            temp := (temp - 64) MOD 256; 
        END IF
	
        // subtract the 42 offset and send the result to the output //
        temp := (temp - 42) MOD 256;
        output[o] := temp;
        o := o + 1; 
        // repeat until finished //
    NEXT i; 
    Note: the input is the encoded file. The output is the original file.  
			 
    */
    using System.Text.RegularExpressions;
    using System.Collections;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Summary description for CodecBase
    /// </summary>
    public class CodecBase
    {
        public string HeaderPattern
        {
            get
            {
                return m_HeaderPattern.ToString();
            }
            set
            {
                m_HeaderPattern = new Regex(value, RegexOptions.Multiline | RegexOptions.Compiled);
            }
        }

        public string TrailerPattern
        {
            get
            {
                return m_TrailerPattern.ToString();
            }
            set
            {
                m_TrailerPattern = new Regex(value, RegexOptions.Multiline | RegexOptions.Compiled);
            }
        }

        private Regex[] m_InfoPatterns = new Regex[0];
        public string[] InfoPatterns
        {
            get
            {
                string[] patterns = new string[m_InfoPatterns.Length];

                for (int i = 0; i < m_InfoPatterns.Length; i++)
                {
                    patterns[i] = m_InfoPatterns[i].ToString();
                }

                return patterns;
            }
            set
            {
                m_InfoPatterns = new Regex[value.Length];
                for (int i = 0; i < value.Length; i++)
                {
                    m_InfoPatterns[i] = new Regex(value[i], RegexOptions.Multiline | RegexOptions.Compiled);
                }
            }
        }

        private bool IsInfoLine(string line)
        {
            foreach (Regex r in m_InfoPatterns)
            {
                if (r.IsMatch(line))
                {
                    return true;
                }
            }

            return false;
        }

        protected virtual void InfoLineFound(string line)
        {
        }

        protected Regex m_HeaderPattern = new Regex(@".*", RegexOptions.Multiline | RegexOptions.Compiled);
        protected Regex m_TrailerPattern = new Regex(@".*", RegexOptions.Multiline | RegexOptions.Compiled);

        public CodecBase()
        {
        }

        /// <summary>
        /// Creates a file stream and calls DecodeFileFromStream
        /// </summary>
        /// <param name="path">path of file to decode</param>
        /// <returns>failure/success</returns>
        public bool DecodeFileFromDisk(string path, string diretorioUrl)
        {
            FileStream s = null;
            bool rslt = true;
            try
            {
                s = new FileStream(path, FileMode.Open);
                rslt = DecodeFileFromStream(s, diretorioUrl);
            }
            catch (Exception)
            {
                rslt = false;
            }
            finally
            {
                if (s != null)
                {
                    s.Close();
                }
            }

            return rslt;
        }

        /// <summary>
        /// Reads a stream line by line and decodes any YEncoded sections
        /// </summary>
        /// <param name="s">the stream to read from</param>
        /// <returns>failure/success</returns>
        public bool DecodeFileFromStream(Stream s, string diretorioUrl)
        {
            bool rslt = true;
            FileStream fs = null; // output filestream
            CloseStreamsSafe();
            string fileName = "";
            try
            {
                m_LineReader = new BinaryLineReader(s);
                bool foundStart = false;
                byte[] lineData = m_LineReader.ReadLine();
                bool isInfo = false;

                while (lineData != null)
                {
                    string dat = Encoding.ASCII.GetString(lineData);
                    isInfo = IsInfoLine(dat);
                    if (isInfo)
                    {
                        InfoLineFound(dat);
                    }

                    if (!foundStart)
                    {
                        Match m = this.m_HeaderPattern.Match(dat);
                        if (m.Success)
                        {
                            foundStart = true;
                            fileName = m.Groups[1].Value;

                            if (!Directory.Exists(diretorioUrl))
                                Directory.CreateDirectory(diretorioUrl);

                            fs = new FileStream(diretorioUrl + fileName, FileMode.Create);
                        }
                        lineData = m_LineReader.ReadLine();
                        continue;
                    }
                    else
                    {
                        // Found start, see if this is the trailer line
                        Match m = this.m_TrailerPattern.Match(dat);
                        if (m.Success)
                        {
                            // Found the end!
                            break;
                        }

                        // See if the line is an informational line if so, process the info and keep reading
                        if (isInfo)
                        {
                            lineData = m_LineReader.ReadLine();
                            continue;
                        }
                    }

                    // when we get here, we should be reading encoded data
                    if (fs == null)
                    {
                        break;
                    }

                    byte[] curdat = Decode(lineData);
                    fs.Write(curdat, 0, curdat.Length);
                    lineData = m_LineReader.ReadLine();
                }
            }

            catch (Exception)
            {
                rslt = false;
            }

            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }

                CloseStreamsSafe();
            }

            return rslt;
        }

        public bool DecodeFileFromString(string contents, string diretorioUrl)
        {
            bool rslt = true;
            ArrayList outData = new ArrayList();
            MemoryStream ms = null;
            try
            {
                CloseStreamsSafe();
                byte[] binData = new byte[contents.Length];
                Encoding.ASCII.GetBytes(contents, 0, contents.Length, binData, 0);
                ms = new MemoryStream(binData);
                DecodeFileFromStream(ms, diretorioUrl);
            }
            catch (Exception decExc)
            {
                System.Diagnostics.Debug.WriteLine("Error decoding: " + decExc.Message);
                rslt = false;
            }
            finally
            {
                if (ms != null)
                {
                    ms.Close();
                }

                CloseStreamsSafe();
            }

            return rslt;
        }

        private BinaryLineReader m_LineReader;

        private void CloseStreamsSafe()
        {
            if (m_LineReader != null)
            {
                m_LineReader.Close();
            }
        }

        protected virtual byte[] Decode(byte[] data)
        {
            return data;
        }
    }
}
