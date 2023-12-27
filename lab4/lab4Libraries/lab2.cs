using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4libraries
{
    public class lab2
    {
        public string inputPath, outputPath;
        public lab2(string input, string output)
        {
            inputPath = input;
            outputPath = output;
        }
        public void lab2Solution()
        {

            string[] inputLines = File.ReadAllLines(inputPath);
            int n = int.Parse(inputLines[0].Split()[0]);
            int k = int.Parse(inputLines[0].Split()[1]);

            long result = CountBeautifulSequences(n, k);

            File.WriteAllText(outputPath, result.ToString());
        }

        static long CountBeautifulSequences(int n, int k)
        {
            long[,] dp = new long[n + 1, k + 1];

            for (int i = 1; i <= k; i++)
            {
                dp[1, i] = 1;
            }

            for (int i = 2; i <= n; i++)
            {
                for (int j = 1; j <= k; j++)
                {
                    for (int m = 1; m <= k; m++)
                    {
                        if (j == 1 || m == 1 || j == m + 1 || j == m - 1)
                        {
                            dp[i, j] += dp[i - 1, m];
                        }
                    }
                }
            }

            long result = 0;
            for (int i = 1; i <= k; i++)
            {
                result += dp[n, i];
            }

            return result;
        }
    }
}
