using System;
using System.IO;

class Program
{
    static void Main()
    {
        string inputPath = "../../../INPUT.TXT";
        string outputPath = "../../../OUTPUT.TXT";

        // Читання вхідних даних з файлу
        string[] inputLines = File.ReadAllLines(inputPath);
        int n = int.Parse(inputLines[0].Split()[0]);
        int k = int.Parse(inputLines[0].Split()[1]);

        // Знаходження кількості особливо гарних послідовностей
        long result = CountBeautifulSequences(n, k);

        // Запис відповіді у вихідний файл
        File.WriteAllText(outputPath, result.ToString());
    }

    static long CountBeautifulSequences(int n, int k)
    {
        // Ініціалізація динамічного масиву для зберігання кількості гарних послідовностей
        long[,] dp = new long[n + 1, k + 1];

        // Початкові значення для n=1
        for (int i = 1; i <= k; i++)
        {
            dp[1, i] = 1;
        }

        // Заповнення динамічного масиву
        for (int i = 2; i <= n; i++)
        {
            for (int j = 1; j <= k; j++)
            {
                for (int m = 1; m <= k; m++)
                {
                    // Перевірка умови для гарної послідовності
                    if (j == 1 || m == 1 || j == m + 1 || j == m - 1)
                    {
                        // Оновлення кількості гарних послідовностей
                        dp[i, j] += dp[i - 1, m];
                    }
                }
            }
        }

        // Сума кількостей гарних послідовностей для всіх k на останньому кроці
        long result = 0;
        for (int i = 1; i <= k; i++)
        {
            result += dp[n, i];
        }

        return result;
    }
}
