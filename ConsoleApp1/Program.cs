using System;

namespace ConsoleApp1
{
    class Program
    {
        /*static void Main(string[] args)
        {
            int p = 17;
            int q = 11;
            int e = 7;

            RSA rsa = new RSA(p, q, e);

            rsa.SaveKeys("C:\\Users\\skude\\source\\repos\\ConsoleApp1\\ConsoleApp1\\publickey.txt");

            string inputFile = "C:\\Users\\skude\\source\\repos\\ConsoleApp1\\ConsoleApp1\\input.txt";
            string encryptedFile = "C:\\Users\\skude\\source\\repos\\ConsoleApp1\\ConsoleApp1\\encrypted.txt";
            string decryptedFile = "C:\\Users\\skude\\source\\repos\\ConsoleApp1\\ConsoleApp1\\decrypted.txt";

            rsa.Encrypt(inputFile, encryptedFile);
            rsa.Decrypt(encryptedFile, decryptedFile);
        }*/
        static void Main(string[] args)
        {


            string plaintext = "C:\\Users\\skude\\source\\repos\\ConsoleApp1\\ConsoleApp1\\input.txt";
            string encryptedFile = "C:\\Users\\skude\\source\\repos\\ConsoleApp1\\ConsoleApp1\\encrypted.txt";
            string decryptedFile = "C:\\Users\\skude\\source\\repos\\ConsoleApp1\\ConsoleApp1\\decrypted.txt";
            string publicKeyFile = "C:\\Users\\skude\\source\\repos\\ConsoleApp1\\ConsoleApp1\\publickey.txt";
            // Step 1: Input initial data
            int p = 17; // Example value
            int q = 11; // Example value

            // Step 2: Calculate RSA parameters
            RSA rsa = new RSA(p, q); // Example e value

            // Step 3: Encrypt plaintext x
            rsa.Encrypt(plaintext, encryptedFile);

            // Step 4: Save encrypted text and public key to file
            rsa.SaveKeys(publicKeyFile);

            // Step 5: Read encrypted text and public key from file
            int[] publicKey = RSA.ReadPublicKey(publicKeyFile);
            Console.WriteLine("Public key: " + publicKey[0] + "," + publicKey[1]);

            // Step 6: Decrypt encrypted text using private key
            int[] primes = RSA.FindPrimes(publicKey[0]);
            Console.WriteLine("Primes: " + primes[0] + "," + primes[1]);
            RSA rsa2 = new RSA(primes[0], primes[1]);
            Console.WriteLine(rsa2.e);
            rsa2.Decrypt(encryptedFile, decryptedFile);

            // Print decrypted text to console
            byte[] decryptedBytes = System.IO.File.ReadAllBytes(decryptedFile);
            string decryptedText = System.Text.Encoding.UTF8.GetString(decryptedBytes);
            Console.WriteLine("Decrypted text: " + decryptedText);
        }
    }
}