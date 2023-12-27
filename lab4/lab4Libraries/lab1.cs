using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4libraries
{
    public class lab1
    {
        public static void Main(string[] args) { }
        public string inputPath, outputPath;

        public lab1(string input, string output)
        {
            inputPath = input;
            outputPath = output;
        }
        public void lab1Solution()
        {
            using (StreamReader reader = new StreamReader(inputPath))
            {
                string[] values = reader.ReadLine().Split(' ');
                int n = int.Parse(values[0]);
                int k = int.Parse(values[1]);
                int p = int.Parse(values[2]);

                string word = reader.ReadLine();

                Func<char, string>[] morphisms = new Func<char, string>[n];
                for (int i = 0; i < n; i++)
                {
                    morphisms[i] = GetMorphismFunction(reader, i);
                }

                char result = GetCharacter(word, morphisms, k, p);

                using (StreamWriter writer = new StreamWriter(outputPath))
                {
                    writer.Write(result);
                }
            }
        }

        static Func<char, string> GetMorphismFunction(StreamReader reader, int i)
        {
            if (i + 2 < reader.BaseStream.Length)
            {
                return c => reader.ReadLine();
            }
            else
            {
                return _ => "";
            }
        }

        static char GetCharacter(string word, Func<char, string>[] morphisms, int k, int p)
        {
            string result = word;

            for (int i = 0; i < k; i++)
            {
                string temp = "";
                foreach (char c in result)
                {
                    temp += morphisms[Math.Min(c - 'A', morphisms.Length - 1)](c);
                }
                result = temp;
            }

            if (p <= result.Length)
            {
                return result[p - 1];
            }
            else
            {
                return '-';
            }
        }
    }
}
