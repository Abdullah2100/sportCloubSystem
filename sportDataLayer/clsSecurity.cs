using System;
using System.Security.Cryptography;
using System.Text;
namespace sportDataLayer
{
    static class clsSecurity
    {
        static string hashInput(string input)
        {
            using (SHA256 shaInput = SHA256.Create())
            {
                byte[] hashInput = shaInput.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(hashInput).Replace('-', ' ').ToLower();
            }

        }

        static bool isValidHash(string plainText, string hashtext)
        {
            return hashInput(plainText) == (hashtext);
        }

    }
}
