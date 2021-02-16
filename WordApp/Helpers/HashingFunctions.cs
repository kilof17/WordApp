using System;
using System.Linq;
using System.Security.Cryptography;// SHA256
using System.Text;  // Encoding

namespace WordApp
{
    public static class HashingFunctions
    {
        public static string Hash(string chain)
        {
           using( SHA256 sha256 =  SHA256.Create())
            {
                var sha = sha256.ComputeHash(Encoding.UTF8.GetBytes(chain));            
                string hashedString = Convert.ToBase64String(sha);
                return hashedString;
            }
        }     

        public static string InfoHash(string chain)
        {
            string infoHash= Hash(chain);
            char[] except = {'/','\'','+','='};
            return new String(infoHash.Except(except).ToArray());
            
        }
    }
}