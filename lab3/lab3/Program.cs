using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        int n;
        using (StreamReader reader = new StreamReader("../../../INPUT.TXT")) //Зчитуємо кількість графів
        {
            n = int.Parse(reader.ReadLine());
        }

        List<string> graphs = new List<string>(); //Створюємо список для зберігання імен графів

        using (StreamReader reader = new StreamReader("../../../INPUT.TXT")) //Зчитуємо імена графів та додаємо їх до списку
        {
            reader.ReadLine(); //Пропускаємо перший рядок, оскільки вже зчитали кількість графів

            while (!reader.EndOfStream) //Зчитуємо імена графів та додаємо їх до списку
            {
                string graphName = reader.ReadLine().Trim(); //Видаляємо можливі пробіли
                graphs.Add(graphName);
            }
        }

        int result = FindMinGraphsToAdd(graphs); //Знаходимо мінімальну кількість графів, які потрібно додати

        using (StreamWriter writer = new StreamWriter("../../../OUTPUT.TXT")) //Виводимо результат у вихідний файл
        {
            writer.WriteLine(result);
        }
    }

    static int FindMinGraphsToAdd(List<string> graphs)
    {
        Dictionary<char, int> letterCount = new Dictionary<char, int>(); //Створюємо словник для зберігання літер та кількості входжень кожної літери

        foreach (string graphName in graphs) //Ітеруємося по іменах графів та обчислюємо кількість входжень кожної літери
        {
            char lastLetter = graphName[graphName.Length - 1];
            char firstLetter = graphName[graphName.Length - 3];

            if (!letterCount.ContainsKey(lastLetter))
                letterCount[lastLetter] = 0;

            if (!letterCount.ContainsKey(firstLetter))
                letterCount[firstLetter] = 0;

            letterCount[lastLetter]++;
        }

        int result = 0;
        foreach (var count in letterCount.Values) //Знаходимо кількість графів, які треба додати, як суму абсолютних значень кількостей літер
        {
            result += Math.Abs(count);
        }
        return result / 2; //Результат - половина кількості графів, які треба додати
    }
}
