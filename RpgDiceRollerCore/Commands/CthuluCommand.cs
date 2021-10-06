using RpgDiceRollerCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RpgDiceRollerCore.Commands
{
    public class CthuluCommand : ICommand
    {
        bool active;
        int dc;
        int success;
        int hardSuccess;
        int critialSuccess;

        string pattern = @"--cth ([0-9]+) ?";
        public CthuluCommand()
        {

        }

        public string Directions()
        {
            return pattern;
        }

        public string Final()
        {
            if (active)
                return $"Success: {success}; Hard Success {hardSuccess}; Extreme Success {critialSuccess}";
            return null;
        }

        public void Init(string input)
        {
            active = Regex.IsMatch(input, pattern);
            if(active)
            {
                var match = Regex.Match(input, pattern);
                dc = int.Parse(match.Groups[1].Value);
            }
        }

        public string onEval(int result)
        {
            if (active)
            {
                if (Math.Floor((decimal)(dc / 5)) >= result)
                {
                    critialSuccess++;
                    return "Extreme Success";
                }
                else if (Math.Floor((decimal)(dc / 2)) >= result)
                {
                    hardSuccess++;                   
                    return "Hard Success";
                }
                else if(dc >= result)
                {
                    success++;
                    return "Success";
                }
                return "Miss";
            }
            return null;
        }

        public string remove(string input)
        {

            return Regex.Replace(input, pattern, string.Empty);
        }
    }
}
