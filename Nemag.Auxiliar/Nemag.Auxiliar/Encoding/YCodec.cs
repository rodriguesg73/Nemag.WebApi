using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace Nemag.Auxiliar.Encoding
{
	/// <summary>
	/// Summary description for YCodec.
	/// </summary>
    public class YCodec : CodecBase
    {
		public YCodec()
		{
			this.HeaderPattern = @"^\=ybegin .*size=(?<2>\d+) .*name=(?<1>.+)$";
			this.TrailerPattern = @"^\=yend .*size=(?<2>\d+).*$";
			string[] bleh = new string[1];
			bleh[0] = @"^\=y.+";
			this.InfoPatterns = bleh;			
		}
		
		protected override void InfoLineFound(string dat)
		{
			Match m = m_HeaderPattern.Match(dat);
			if(m.Success)
			{
				m_ReportedFileSize = Int32.Parse(m.Groups[2].Value);
			}
		}

		private int m_ReportedFileSize = 0;


		protected override byte[] Decode(byte[] line)
		{
			ArrayList output = new ArrayList();
			int lineLength = line.Length;
			
			/*
			if(lineLength % 128 > 1)
			{
				throw new Exception("Encountered bad line length while decoding.");
			}
			*/

			byte cTmp;

			for(int i = 0; i < lineLength; i++)
			{
				cTmp = line[i];
				if(cTmp == '=')
				{
					i++;
					cTmp = line[i];
					cTmp = (byte)((cTmp - 64) % 256);
				}

				cTmp = (byte)((cTmp - 42) % 256);
				output.Add(cTmp);
			}

			byte[] outData = (byte[])output.ToArray(typeof(byte));
			return outData;
		}
		
	}
}
