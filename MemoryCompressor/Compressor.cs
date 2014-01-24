using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;

//Taken 7:47 AM 1/22/2010
namespace MemoryCompressor
{
    public class Compressor
    {
        public static byte[] Compress(byte[] buffer, int offset, int count)
        {

            MemoryStream ms = new MemoryStream();
            GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true);

            zip.Write(buffer, offset, count);
            zip.Close();
            ms.Position = 0;

            //+ 4 so we can hold the length of orginal data
            byte[] compressed = new byte[ms.Length + 4];
            //Copy in above at the begining of the buffer
            Buffer.BlockCopy(BitConverter.GetBytes(count), 0, compressed, 0, 4);

            //Copy in the rest of the compressed data
            ms.Read(compressed, 4, (int)ms.Length);

            return compressed;
        }

        public static byte[] Decompress(byte[] gzBuffer)
        {
            MemoryStream ms = new MemoryStream();

            //Grab what the length should be of orginal data
            int msgLength = BitConverter.ToInt32(gzBuffer, 0);

            //Put the rest of the incoming data into the the memory stream
            ms.Write(gzBuffer, 4, gzBuffer.Length - 4);

            ms.Position = 0;
            GZipStream zip = new GZipStream(ms, CompressionMode.Decompress);

            //Create buffer accordingly and uncompress
            byte[] buffer = new byte[msgLength];
            zip.Read(buffer, 0, buffer.Length);

            return buffer;
        }
    }
}
