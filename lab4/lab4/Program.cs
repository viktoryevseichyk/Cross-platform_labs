using lab4libraries;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var console = new CommandLineApplication(); //Створюємо об'єкт для роботи з командним рядком

            console.HelpOption(); //Встановлюємо опцію довідки для командного рядка

            console.Command("version", command => { //Додаємо команду "version" для виведення інформації про автора та версію
                command.Description = "Show author and version"; //Створюємо опис комаанди
                command.OnExecute(() => Console.WriteLine("Lab4\nVersion: 1.0 Author: Viktor Yevseichyk")); // Визначаємо дії при виконанні команди

            });

            console.Command("set-path", command => { //Додаємо команду "set-path" для встановлення шляху до вхідних та вихідних файлів
                command.Description = "Set path to input and output files"; //Створюємо опис комаанди

                var folderPathOption = command.Option("-p|--path", "Set path to input and output files", CommandOptionType.SingleValue); //Встановлюємо опцію для задання шляху

                folderPathOption.IsRequired(); //Опція є обов'язковою

                command.OnExecute(() => { //Визначаємо дії при виконанні команди
                    var folderPath = folderPathOption.Value();
                    System.Environment.SetEnvironmentVariable("LAB_PATH", folderPath, EnvironmentVariableTarget.User);
                    Console.WriteLine($"Path to files has been set to: {folderPath}");
                });
            });

            console.Command("run", command => // Додаємо команду "run" для виконання lab1, lab2 або lab3
            {
                command.Description = "Run lab1, lab2 or lab3";  //Створюємо опис комаанди

                var runLab = command.Argument("lab", "Wich lab to run"); //Визначаємо аргумент для вибору лабораторної роботи

                var inputFile = command.Option("-i|--input <path>", "Path to input file", CommandOptionType.SingleValue); //Опція для задання шляху до вхідного файлу
                var outputFile = command.Option("-o|--output <path>", "Path to output file", CommandOptionType.SingleValue); //Опція для задання шляху до вихідного файлу

                command.OnExecute(() => { //Визначаємо дії при виконанні команди
                    var inputFilePath = inputFile.Value();
                    var outputFilePath = outputFile.Value();

                    if (string.IsNullOrEmpty(inputFilePath)) //Якщо шлях до вхідного файлу не заданий, спробуємо знайти його в середовищі або встановимо значення за замовчуванням
                    {
                        inputFilePath = Environment.GetEnvironmentVariable("LAB_PATH", EnvironmentVariableTarget.User);

                        if (string.IsNullOrEmpty(inputFilePath))
                        {
                            inputFilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

                            if (!string.IsNullOrEmpty(inputFilePath))
                            {
                                inputFilePath = Path.Combine(inputFilePath, "input.txt");
                            }
                        }
                        else
                        {
                            inputFilePath = Path.Combine(inputFilePath, "input.txt");
                        }
                    }

                    if (string.IsNullOrEmpty(outputFilePath)) //Аналогічно для вихідного файлу
                    {
                        outputFilePath = Environment.GetEnvironmentVariable("LAB_PATH", EnvironmentVariableTarget.User);

                        if (string.IsNullOrEmpty(outputFilePath))
                        {
                            outputFilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

                            if (!string.IsNullOrEmpty(outputFilePath))
                            {
                                outputFilePath = Path.Combine(outputFilePath, "input.txt");
                            }
                        }
                        else
                        {
                            outputFilePath = Path.Combine(outputFilePath, "input.txt");
                        }
                    }
                     
                    if (File.Exists(inputFilePath)) //Перевіряємо наявність вхідного файлу
                    {
                        string execLabValue = runLab.Value;

                        if (string.IsNullOrEmpty(execLabValue) || (execLabValue != "lab1" && execLabValue != "lab2" && execLabValue != "lab3"))
                        {
                            Console.WriteLine("!ERROR! Invalid lab value \nChoose lab1, lab2 or lab3");
                        }
                        else
                        {
                            if (execLabValue == "lab1") //Викликаємо відповідний метод відповідно до обраної лабораторної роботи
                            {
                                lab1 LabRob1 = new lab1(inputFilePath, outputFilePath);
                                LabRob1.lab1Solution();
                            }
                            else if (execLabValue == "lab2")
                            {
                                lab2 LabRob2 = new lab2(inputFilePath, outputFilePath);
                                LabRob2.lab2Solution();
                            }
                            else
                            {
                                lab3 LabRob3 = new lab3(inputFilePath, outputFilePath);
                                LabRob3.lab3Solution();
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Can`t find input file by your path: {inputFilePath}");
                        command.ShowHelp();
                    }
                });
            });

            console.OnExecute(() => console.ShowHelp()); //Додаємо обробник для виведення довідки в разі відсутності команди

            return console.Execute(args); //Викликаємо виконання командного рядка і повертаємо результат
        }
    }
}
