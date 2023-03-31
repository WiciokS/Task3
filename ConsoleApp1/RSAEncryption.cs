using System;
using System.IO;

public class RSA
{
    private int p;
    private int q;
    private int n;
    public int e { get; set; }
    private int d;
    private int phi;

    public RSA(int p, int q)
    {
        this.p = p;
        this.q = q;
        this.n = p * q;

        this.phi = (p - 1) * (q - 1);
        this.e = calculateE(phi);
        this.d = CalculateD(e, phi);
    }

    private int CalculateD(int e, int phi)
    {
        var PrivateKey = 1;
        while (true)
        {
            if (PrivateKey * e % phi == 1)
            {
                break;
            }

            PrivateKey++;
        }
        return PrivateKey;
    }

    public void Encrypt(string inputFile, string outputFile)
    {
        byte[] inputBytes = File.ReadAllBytes(inputFile);
        byte[] outputBytes = new byte[inputBytes.Length];

        for (int i = 0; i < inputBytes.Length; i++)
        {
            int c = inputBytes[i];
            int x = ModuloPow(c, e, n);
            outputBytes[i] = (byte)x;
        }

        File.WriteAllBytes(outputFile, outputBytes);
    }

    public void Decrypt(string inputFile, string outputFile)
    {
        byte[] inputBytes = File.ReadAllBytes(inputFile);
        byte[] outputBytes = new byte[inputBytes.Length];

        for (int i = 0; i < inputBytes.Length; i++)
        {
            int c = inputBytes[i];
            int x = ModuloPow(c, d, n);
            outputBytes[i] = (byte)x;
        }

        File.WriteAllBytes(outputFile, outputBytes);
    }

    private int ModuloPow(int b, int e, int m)
    {
        int result = 1;

        while (e > 0)
        {
            if ((e & 1) == 1)
            {
                result = (result * b) % m;
            }

            e >>= 1;
            b = (b * b) % m;
        }

        return result;
    }

    public void SaveKeys(string publicKeyFile)
    {
        string publicKey = n + "," + e;
        File.WriteAllText(publicKeyFile, publicKey);
    }

    public int[] GetPrivateKey()
    {
        return new int[] { n, d };
    }

    public static int[] FindPrimes(int n)
    {
        int[] primes = new int[2];

        for (int i = n; i >= 2; i--)
        {
            if (IsPrime(i))
            {
                if (n % i == 0)
                {
                    primes[0] = i;
                    primes[1] = n / i;
                    break;
                }
            }
        }

        return primes;
    }

    public static int[] ReadPublicKey(string publicKeyFile)
    {
        string publicKey = File.ReadAllText(publicKeyFile);
        string[] parts = publicKey.Split(',');

        int n = int.Parse(parts[0]);
        int e = int.Parse(parts[1]);

        return new int[] { n, e };
    }
    public static bool IsPrime(int n)
    {
        if (n <= 1) return false;
        if (n <= 3) return true;

        if (n % 2 == 0 || n % 3 == 0) return false;

        for (int i = 5; i * i <= n; i += 6)
        {
            if (n % i == 0 || n % (i + 2) == 0) return false;
        }

        return true;
    }
    public static int calculateE(int phi)
    {
        for (int e = 2; e < phi; e++)
        {
            if (gcd(e, phi) == 1)
            {
                return e;
            }
        }
        return -1; // no suitable value of e found
    }

    // method to calculate greatest common divisor of two numbers
    private static int gcd(int a, int b)
    {
        while (true)
        {
            if (b == 0) return a;
            var a1 = a;
            a = b;
            b = a1 % b;
        }
    }
}