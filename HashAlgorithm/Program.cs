using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Diagnostics;

namespace HashAlgorithmTest
{
    class Program
    {
        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/0ss79b2x(v=vs.110).aspx
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            byte[] hash1 = GenerateHash("HashAlgorithm.exe.config", "Sha1");
            byte[] hash2 = GenerateHashMultiblock("HashAlgorithm.exe.config", "Sha1", 12);

            Debug.Assert(hash1.SequenceEqual(hash2), string.Format("{0}{2}{1}", BitConverter.ToString(hash1), BitConverter.ToString(hash2), Environment.NewLine));

            Console.ReadKey();
        }

        public static byte[] GenerateHash(string filename, string hashAlgorithm)
        {
            var signatureAlgo = HashAlgorithm.Create(hashAlgorithm);
            var fileBuffer = System.IO.File.ReadAllBytes(filename);
            var hashedBytes = signatureAlgo.ComputeHash(fileBuffer);

            string hash = BitConverter.ToString(hashedBytes);
            //string hash2 = BitConverter.ToString(signatureAlgo.Hash);

            return hashedBytes;
        }

        public static byte[] GenerateHashMultiblock(string filename, string hashAlgorithm, int size)
        {
            var hasher = HashAlgorithm.Create(hashAlgorithm);
            var fileBuffer = System.IO.File.ReadAllBytes(filename);
            int offset = 0;

            while (fileBuffer.Length - offset >= size)
                offset += hasher.TransformBlock(fileBuffer, offset, size, fileBuffer, offset);

            hasher.TransformFinalBlock(fileBuffer, offset, fileBuffer.Length - offset);


            return hasher.Hash;
        }

    }
}
