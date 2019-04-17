using RpgDiceRollerCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RpgDiceRollerCore.Commands
{
    public class SumCommand : ICommand
    {
        bool active;
        int total;
        string pattern = "--sum";
        public SumCommand()
        {

        }

        public string Directions()
        {
            return "--sum";
        }

        public string Final()
        {
            if(active)
                return string.Format("sum: {0}", total);
            return null;
        }

        public void Init(string input)
        {
            active = input.Contains(pattern);
        }

        public string onEval(int result)
        {
            if (active)
                total += result;
            return null;
        }

        public string remove(string input)
        {
            return Regex.Replace(input, @"--sum ?", string.Empty);
        }
    }
}
