using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace CritoZLP
{
    public class Encryption
    {
        private const string Key = "12345678";
        private readonly int[] _permutation;

        public Encryption(int[] permutation)
        {
            if (permutation.Length != 3)
            {
                throw new ArgumentException("Permutation length must be 3.", nameof(permutation));
            }
            _permutation = permutation;
        }

        public void Encrypt(string inputFile, string outputFile)
        {
            byte[] inputBytes = File.ReadAllBytes(inputFile);
            byte[] encryptedBytes = new byte[inputBytes.Length];
            byte[] keyBytes = Encoding.UTF8.GetBytes(Key);

            // Гаммирование
            for (int i = 0; i < inputBytes.Length; i++)
            {
                encryptedBytes[i] = (byte)(inputBytes[i] ^ keyBytes[i % keyBytes.Length]);
            }

            // Перестановка строк и столбцов матрицы 3x3
            if (encryptedBytes.Length % 9 != 0)
            {
                throw new ArgumentException("File size must be a multiple of 9 for matrix permutation.");
            }

            byte[] permutedBytes = new byte[encryptedBytes.Length];

            for (int i = 0; i < encryptedBytes.Length; i += 9)
            {
                for (int j = 0; j < 9; j++)
                {
                    permutedBytes[i + _permutation[j / 3] * 3 + j % 3] = encryptedBytes[i + j];
                }
            }

            File.WriteAllBytes(outputFile, permutedBytes);
        }

        public void Decrypt(string inputFile, string outputFile)
        {
            byte[] inputBytes = File.ReadAllBytes(inputFile);
            byte[] decryptedBytes = new byte[inputBytes.Length];
            byte[] keyBytes = Encoding.UTF8.GetBytes(Key);

            // Обратная перестановка строк и столбцов матрицы 3x3
            if (inputBytes.Length % 9 != 0)
            {
                throw new ArgumentException("File size must be a multiple of 9 for matrix permutation.");
            }

            byte[] unpermutedBytes = new byte[inputBytes.Length];

            for (int i = 0; i < inputBytes.Length; i += 9)
            {
                for (int j = 0; j < 9; j++)
                {
                    unpermutedBytes[i + j] = inputBytes[i + _permutation[j / 3] * 3 + j % 3];
                }
            }

            // Обратное гаммирование
            for (int i = 0; i < unpermutedBytes.Length; i++)
            {
                decryptedBytes[i] = (byte)(unpermutedBytes[i] ^ keyBytes[i % keyBytes.Length]);
            }

            File.WriteAllBytes(outputFile, decryptedBytes);
        }
    }
}
