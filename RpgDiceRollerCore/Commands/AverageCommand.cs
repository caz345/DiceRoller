using RpgDiceRollerCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RpgDiceRollerCore.Commands
{
    public class AverageCommand : ICommand
    {
        bool active;
        int sum;
        int iterations;
        string pattern = @"--avg ?";

        public AverageCommand()
        { }


        public string Directions()
        {
            return pattern;
        }

        public string Final()
        {
            if (active)
            {
                var average = sum / iterations;
                return string.Format("Average: {0}", average);
            }
            return null;
        }

        public void Init(string input)
        {
            active = Regex.IsMatch(input, pattern);
        }

        public string onEval(int result)
        {
            if(active)
            {
                sum += result;
                iterations++;
            }
            return null;
        }

        public string remove(string input)
        {
            return Regex.Replace(input, pattern, string.Empty);
        }
    }
}
