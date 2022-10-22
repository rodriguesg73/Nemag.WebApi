using System.IO;
using System;

namespace Nemag.Auxiliar.Encoding
{
	class BinaryLineReader
	{
		private static int	 BUFFER_SIZE		  = 8192; // Initial buffer size and increase amount
		private int			 m_bufferDataPointer  = 0; // Up to which element in the buffer do we have actual data
		private int			 m_bufferReadPointer  = 0; // Up to which point in the buffer have we read
		private byte[]		 m_buffer             = new byte[BUFFER_SIZE];
		private Stream		 m_stream             = null;
		private BinaryReader m_br				  = null;
	
		public BinaryLineReader(Stream s) 
		{
			m_stream = s;
			m_br = new BinaryReader(m_stream, System.Text.Encoding.ASCII);
		}

		~BinaryLineReader() {}

		public void Close()
		{
			if(m_stream != null)
			{
				m_stream.Close();
			}

			if(m_br != null)
			{
				m_br.Close();
			}
		}

		// Reads the next line in the buffer and sees if it is a \n	     
		private bool IsNextByteLineFeed()
		{
			bool rslt = false;
			if(m_buffer.GetUpperBound(0) >= (m_bufferReadPointer +1)) // then we can safely check the next char
			{
				if(m_buffer[m_bufferReadPointer+1] == (byte)'\n')
				{
					rslt = true;
				}			
			}

			return rslt;
		}

		/// <summary>
		/// Reads data fron the member stream into the buffer - increases buffer size if necessary
		/// </summary>
		/// <returns>the number of bytes read from the stream</returns>
		private int ReadNextDataIntoBuffer()
		{
			// We need to increase size of buffer?
			if ((m_bufferDataPointer + BUFFER_SIZE) > m_buffer.Length) 
			{
				byte[] tmp = new Byte[m_buffer.Length + BUFFER_SIZE]; 
				Array.Copy(m_buffer, m_bufferReadPointer, tmp, 0, m_buffer.Length);
				m_buffer = tmp;
				tmp = null;
			}
	
			int read = m_br.Read(m_buffer, m_bufferDataPointer, BUFFER_SIZE);
			m_bufferDataPointer += read;

			return(read);		
		}

		public byte[] GetLineData()
		{
			byte[] tmp = new byte[m_bufferReadPointer];
			Array.Copy(m_buffer, 0, tmp, 0, tmp.Length);
			m_bufferReadPointer += 2; // move beyond \n
			return tmp;
		}

		/// <summary>
		/// Reads one line of binary data until a \r\n pair is encountered and advanced the stream pointer.
		/// If no \r\n sequence is encountered, the entire stream will be returned
		/// </summary>
		/// <returns>One line of binary data, delimited by a 13/10 byte sequence, not including the delimiters</returns>
		public byte [] ReadLine ()
		{
			byte[] output = null;
			int end       = 0;
			int dataRead  = 0;

			do
			{
				// Do we have a \r ?
				end = Array.IndexOf(m_buffer, (byte)'\r', m_bufferReadPointer, (m_bufferDataPointer - m_bufferReadPointer));
			
				if(end > -1)
				{
					m_bufferReadPointer = end;
				
					// Next byte \n?
					if(IsNextByteLineFeed())
					{
						output = GetLineData();

						/* moved this line into GetLineData, should be okay. */
						//m_bufferReadPointer = end + 2; // move beyond \n	

						// Copy the data down
						Array.Copy(m_buffer, m_bufferReadPointer, m_buffer, 0, m_bufferDataPointer - m_bufferReadPointer);
						Array.Clear(m_buffer, m_bufferDataPointer - m_bufferReadPointer, m_bufferReadPointer);
						m_bufferDataPointer = m_bufferDataPointer - m_bufferReadPointer;
						m_bufferReadPointer = 0;
						break;
					}

					m_bufferReadPointer = end;
				}

				dataRead = ReadNextDataIntoBuffer();

			}while(dataRead > 0);

			if(output == null) // then we were unable to find a \r\n pair by the time we ran out of stream data
			{
				output = new byte[m_bufferDataPointer];
				if(output.Length < 1)
				{
					throw new IOException("Unexpected end of data.");
				}
				Array.Copy(m_buffer, 0, output, 0, output.Length);
			}

			return output;
		}
	}
}