using System;

namespace RetroGames.Crypto.RC4
{
    public class Scrambler
    {
        public Scrambler(ulong seed)
        {
            Ix = 0;
            Buffer = SeedBuffer(seed);
        }

        public int Ix { get; set; }

        public ulong[] Buffer { get; set; }

        public int GetByte
        {
            get
            {
                var x = (ulong) GetInt;
                if (IsNeg(x))
                    x = Negate(x);
                return (int) (x % 256);
            }
        }

        public int GetInt
        {
            get
            {
                if (Ix == 0)
                    MixBuffer();
                var val = Buffer[Ix];

                Ix = (Ix + 1) % 624;
                val ^= RShift(val, 11) ^ (LShift(val ^ RShift(val, 11), 7) & 0x9D2C5680);
                return (int) (RShift(val ^ (LShift(val, 15L) & 0xEFC60000), 18L) ^ val ^
                              (LShift(val, 15L) & 0xEFC60000));
            }
        }

        public static ulong[] SeedBuffer(ulong seed)
        {
            var buffer = new ulong[624];
            for (var i = 0; i < 624; i++)
            {
                buffer[i] = seed;
                seed = (1812433253 * ((seed ^ RShift(seed, 30)) + 1)) & 0xFFFFFFFF;
            }
            return buffer;
        }

        public void MixBuffer()
        {
            var i = 0;
            var j = 0;
            while (i < 624)
            {
                i += 1;
                var v4 = (Buffer[i % 624] & 0x7FFFFFFF) + (Buffer[j] & 0x80000000);
                var v6 = RShift(v4, 1) ^ Buffer[(i + 396) % 624];
                if ((v4 & 1) != 0)
                    v6 ^= 0x9908B0DF;
                Buffer[j] = v6;
                j += 1;
            }
        }

        public static ulong RShift(ulong num, ulong n)
        {
            ulong highbits = 0;
            if ((num & Pow(2, 31)) != 0)
                highbits = (Pow(2, n) - 1) * Pow(2, 32 - n);
            return (num / Pow(2, n)) | highbits;
        }

        public static ulong LShift(ulong num, ulong n)
        {
            return num * Pow(2, n) % Pow(2, 32);
        }

        public static bool IsNeg(ulong num)
        {
            return (num & (ulong) Math.Pow(2, 31)) != 0;
        }

        public static ulong Negate(ulong num)
        {
            return ~num + 1;
        }

        public static ulong Pow(ulong x, ulong y)
        {
            return (ulong) Math.Pow(x, y);
        }
    }
}