using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] inputLines = File.ReadAllLines("../../../input.txt");

            string[] values = inputLines[0].Split(' '); //Читаємо значення n, k, p
            int n = int.Parse(values[0]);
            int k = int.Parse(values[1]);
            int p = int.Parse(values[2]);

            string word = inputLines[1]; //Читаємо слово

            Func<char, string>[] morphisms = new Func<char, string>[n]; //Читаємо морфізм для кожної букви
            for (int i = 0; i < n; i++)
            {
                morphisms[i] = GetMorphismFunction(inputLines, i); //Отримуємо функцію морфізму для i символу
            }

            char result = GetCharacter(word, morphisms, k, p); //Обчислюємо p символ слова fk(w)

            File.WriteAllText("../../../output.txt", result.ToString()); //Записуємо результат у вихідний файл
        }

        static Func<char, string> GetMorphismFunction(string[] inputLines, int i)
        {
            if (i + 2 < inputLines.Length) //Перевіряємо, чи індекс не виходить за межі масиву
            {
                return c => inputLines[i + 2]; //Повертаємо лямбда-функцію, яка використовує i рядок для морфізму
            }
            else
            {
                return _ => ""; //Якщо індекс виходить за межі, повертаємо функцію, яка завжди повертає порожній рядок
            }
        }

        static char GetCharacter(string word, Func<char, string>[] morphisms, int k, int p)
        {
            string result = word;

            for (int i = 0; i < k; i++) //Застосовуємо морфізми k разів
            {
                string temp = "";
                foreach (char c in result)
                {
                    temp += morphisms[Math.Min(c - 'A', morphisms.Length - 1)](c); //Використовуємо морфізм для кожної букви
                }
                result = temp;
            }

            if (p <= result.Length) //Перевіряємо, чи p-ий символ існує
            {
                return result[p - 1];
            }
            else
            {
                return '-'; //Якщо p виходить за межі, повертаємо -
            }
        }
    }
}
