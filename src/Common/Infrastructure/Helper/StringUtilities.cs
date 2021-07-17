using CleanApplication.Application.Common.Interfaces;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CleanApplication.Infrastructure.Helper
{
    public class StringUtilities : IStringUtilities
    {
        private static Random random = new Random();

        public virtual string Sha512Hash(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            using (var hash = SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);
                var hashedInputStringBuilder = new StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }

     
        public virtual string MD5Hash(string input)
        {
            // Use input string to calculate MD5 hash
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public virtual string Hash(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder sBuilder = new StringBuilder();

                foreach (byte d in data)
                {
                    sBuilder.Append(d.ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }

        public virtual string GenerateRandom(int length, string chars)
        {
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public virtual string GenerateText(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return GenerateRandom(length, chars);
        }

        public virtual string RandomNumber(int length = 1)
        {
            const string chars = "0123456789";
            return GenerateRandom(length, chars);
        }

        public virtual string RandomAlphabet(int length = 1)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return GenerateRandom(length, chars);
        }
        public static int CreateResnum()
        {
            return (int)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
        }
    }
}
