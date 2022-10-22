using System;


namespace Nemag.Auxiliar.Encoding
{
    public class UUCodec : CodecBase
    {
		public UUCodec()
		{
			this.HeaderPattern = @"^begin\s\d\d\d\s+(?<1>.+)\s*";
			this.TrailerPattern = @"^end$";
		}

		private static byte uudecode(byte c)
		{
			return (byte) ((c - ' ') & 0x3f);
		}

		private static int uudecode(byte[] decoded, int decodedStart, byte[] encoded, int encodedStart, int encodedLength)
		{
			if(encodedLength < 1)
			{
				return 0;
			}
			int n = uudecode(encoded[encodedStart]);
			int expected = ((n+2)/3)*4;
			if( expected > (encodedLength-1))
			{
				// someone trimmed off trailing spaces
				byte[] newEncoded = new byte[expected+1];
				Array.Copy(encoded,encodedStart,newEncoded,0,encodedLength);
				for(int i = encodedLength; i <= expected;i++)
				{
					newEncoded[i] = (byte) ' ';
				}
				return uudecode(decoded, decodedStart, newEncoded, 0, newEncoded.Length);
			}

			// decode in groups of four bytes
			int e = encodedStart + 1;
			int d = decodedStart;
			int c = n;
			while(c > 0)
			{
				byte s0 = uudecode(encoded[e]);
				byte s1 = uudecode(encoded[e+1]);
				byte s2 = uudecode(encoded[e+2]);
				byte s3 = uudecode(encoded[e+3]);
				byte d0 = (byte) ((s0 << 2) | (0x03 & (s1 >> 4)));
				byte d1 = (byte) ((s1 << 4) | (0x0f & (s2 >> 2)));
				byte d2 = (byte) ((s2 << 6) | s3);
				decoded[d] = d0;
				if(c>1)
				{
					decoded[d+1] = d1;
				}
				if(c>2)
				{
					decoded[d+2] = d2;
				}
				e += 4;
				d += 3;
				c -= 3;
			}

			return n;
		}

		protected override byte[] Decode(byte[] data)
		{
			byte[] decoded = new byte[data.Length];
			int length = uudecode(decoded, 0, data, 0, data.Length);
			byte[] decodedDat = new byte[length];
			Array.Copy(decoded, 0, decodedDat, 0, length);
			return decodedDat;
		}
	}
}