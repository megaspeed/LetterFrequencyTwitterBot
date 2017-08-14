using System;
using System.Text.RegularExpressions;
using Ninject;
using TwitterBot;

namespace BotConsole
{
    class Program
    {
        private const string QuiteCommand = "quite";

        private static void Main(string[] args)
        {
            
            Run();
        }

        private static void Run()
        {
            var kernel = new StandardKernel(new TwitterServiceNinjectModule());
            var bot = kernel.Get<LetterFrequencyTwitterBot>();

            while (true)
            {
                try
                {
                    var consoleInput = ReadFromConsole().Trim();
                    if (consoleInput.ToLower() == QuiteCommand)
                        break;

                    if (string.IsNullOrWhiteSpace(consoleInput))
                        continue;

                    var targetAccountName = consoleInput.StartsWith("@") ? consoleInput : "@" + consoleInput;
                    if (!CheckAccountName(targetAccountName))
                    {
                        Console.WriteLine("Введите валидный аккаунт");
                        continue;
                    }

                    Console.WriteLine("Start processing");
                    var task = bot.ProcessTweetsAsync(targetAccountName);
                    task.Wait();
                    Console.WriteLine(task.Result);
                    
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"Ошибка: {exc.Message}{Environment.NewLine}{exc.InnerException?.Message}");
                }
             }
        }

        private static string ReadFromConsole()
        {
            WritePromptMessageToConsole();
            return Console.ReadLine();
        }

        private static void WritePromptMessageToConsole()
        {
            Console.WriteLine($"Введите twitter аккаунт для подсчёта статистики.{Environment.NewLine}Для введите {QuiteCommand} и нажмите Enter.");
        }

        private static bool CheckAccountName(string accountName)
        {
            var regex = new Regex(@"^@?(\w){1,15}$");
            return regex.IsMatch(accountName);
        }

    }
}
