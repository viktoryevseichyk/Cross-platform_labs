using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4libraries
{
    public class lab3
    {
        public string inputPath, outputPath;
        public lab3(string input, string output)
        {
            inputPath = input;
            outputPath = output;
        }
        public void lab3Solution()
        {
            int n;
            using (StreamReader reader = new StreamReader(inputPath))
            {
                n = int.Parse(reader.ReadLine());
            }

            List<string> graphs = new List<string>();

            using (StreamReader reader = new StreamReader(inputPath))
            {
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    string graphName = reader.ReadLine().Trim();
                    graphs.Add(graphName);
                }
            }

            int result = FindMinGraphsToAdd(graphs);

            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                writer.WriteLine(result);
            }
        }

        static int FindMinGraphsToAdd(List<string> graphs)
        {
            Dictionary<char, int> letterCount = new Dictionary<char, int>();

            foreach (string graphName in graphs)
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
            foreach (var count in letterCount.Values)
            {
                result += Math.Abs(count);
            }
            return result / 2;
        }
    }
}
