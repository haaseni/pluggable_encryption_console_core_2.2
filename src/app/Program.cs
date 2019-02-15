using System;
using System.Security.Cryptography;

namespace pluggable_encryption_console_core_2._2
{
    /// <summary>
    /// This program is an example of making the cryptographic algorithm of choice
    /// pluggable. This is being done because cryptographic algorithms can frequently be
    /// made obsolete due to many factors most notably a security vulnerability. By passing
    /// in the cryptographic algorithm into the encryption and decryption methods, it will
    /// make swapping out cryptographic algorithms much easier for an application that uses
    /// them.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Uncomment different lines below to use different
            // types of symmetric encryption algorithms, but notice
            // the underlying encryption code doesn't have to change.

            //var symmetricAlgorithm = DES.Create();
            //var symmetricAlgorithm = TripleDES.Create();
            var symmetricAlgorithm = Aes.Create();

            // Set key and IV
            var key = symmetricAlgorithm?.Key;
            var iv = symmetricAlgorithm?.IV;
            
            var original = "This is my password";
            Console.WriteLine($"Original Value : {original}");

            var encrypted = Cryptography.Encrypt(original, symmetricAlgorithm, key, iv); 
            Console.WriteLine($"Encrypted Value: {encrypted}");

            var decrypted = Cryptography.Decrypt(encrypted, symmetricAlgorithm, key, iv);
            Console.WriteLine($"Decrypted Value: {decrypted}");

            Console.ReadKey();
        }
    }
}
