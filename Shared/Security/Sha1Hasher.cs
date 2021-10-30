using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace FreeCMS.Shared.Security
{
    public class Sha1Hasher
    {
        public static string Hash(string input)
        {
            var hash = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(input));
            return string.Concat(hash.Select(b => b.ToString("x2")));
        }
    }
}