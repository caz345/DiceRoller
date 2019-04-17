using RpgDiceRollerCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RpgDiceRollerCore.Commands
{
    public class DcCommand : ICommand
    {
        bool active;
        int dc;
        int hitCount;
        string pattern = @"--dc ([0-9]+) ?";
        public DcCommand()
        {

        }

        public string Directions()
        {
            return pattern;
        }

        public string Final()
        {
            if (active)
                return string.Format("Hits: {0}", hitCount);
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
                if(dc <= result)
                {
                    hitCount++;
                    return "Hit";
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
