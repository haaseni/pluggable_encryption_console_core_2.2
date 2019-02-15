using System;
using System.IO;
using System.Security.Cryptography;

namespace pluggable_encryption_console_core_2._2
{
    public class Cryptography
    {
        public static string Encrypt(string input, SymmetricAlgorithm symmetricAlgorithm, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(nameof(input));
            if (symmetricAlgorithm == null)
                throw new ArgumentNullException(nameof(symmetricAlgorithm));
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (iv == null)
                throw new ArgumentNullException(nameof(iv));

            byte[] encrypted;

            try
            {
                symmetricAlgorithm.Key = key;
                symmetricAlgorithm.IV = iv;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = symmetricAlgorithm.CreateEncryptor(symmetricAlgorithm.Key, symmetricAlgorithm.IV);

                // Create the streams used for encryption.
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(input);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            finally
            {
                symmetricAlgorithm.Dispose();
            }

            // Return the encrypted string from the memory stream.
            return Convert.ToBase64String(encrypted);
        }

        public static string Decrypt(string cipherText, SymmetricAlgorithm symmetricAlgorithm, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException(nameof(cipherText));
            if (symmetricAlgorithm == null)
                throw new ArgumentNullException(nameof(symmetricAlgorithm));
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (iv == null)
                throw new ArgumentNullException(nameof(iv));

            // Declare the string used to hold the decrypted text.
            string plainText = null;
            var cipherBytes = Convert.FromBase64String(cipherText);

            try
            {
                symmetricAlgorithm.Mode = CipherMode.CBC;
                symmetricAlgorithm.Key = key;
                symmetricAlgorithm.IV = iv;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = symmetricAlgorithm.CreateDecryptor(symmetricAlgorithm.Key, symmetricAlgorithm.IV);

                // Create the streams used for decryption.
                using (var ms = new MemoryStream(cipherBytes))
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (var sr = new StreamReader(cs))
                        {
                            // Read the decrypted bytes from the decrypting stream and place them in a string.
                            plainText = sr.ReadToEnd();
                        }
                    }
                }
            }
            finally
            {
                symmetricAlgorithm.Dispose();
            }

            return plainText;
        }
    }
}
