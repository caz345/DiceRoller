using RpgDiceRollerCore.Helpers;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RpgDiceRollerCore
{
    class Program
    {
        static Random roller;
        static void Main(string[] args)
        {
            roller = new Random();
            while (true)
            {
                try {
                    //Clean input
                    var input = Console.ReadLine().ToLower();
                    //input = Regex.Replace(input, @"\s+", "");
                    var commandMod = new CommandModerator();

                    //Check for system commands
                    if (input == "exit")
                        Environment.Exit(0);
                    if (input == "clear")
                    {
                        Console.Clear();
                        continue;
                    }
                    if(input == "help")
                    {
                        Console.WriteLine(commandMod.Help());
                        continue;
                    }
                    commandMod.Init(input);
                    //Check for and remove formula commands
                    input = commandMod.RemoveCommands(input);

                    if (string.IsNullOrEmpty(input))
                        continue;

                    var expressionList = ParserHelper.GetExpressionList(input);

                    Console.WriteLine("-----------");


                    //Build, calculate, and display expression tree
                    foreach (var expression in expressionList)
                    {
                        Console.WriteLine(ParserHelper.GetOutString(expression, commandMod));
                    }
                    var finalOutString = commandMod.FinalOutput();
                    if (finalOutString != null) Console.WriteLine(finalOutString);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Input was invalid");
                }
                catch(Exception)
                {
                    Console.WriteLine("What the hell did you do?");
                }
                Console.WriteLine("-----------");
            }
        }

    }
}
