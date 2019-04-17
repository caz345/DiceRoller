using ExpressionParserCore;
using ExpressionParserCore.Models;
using RpgDiceRollerCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RpgDiceRollerCore.Commands
{
    public class ToHitCommand : ICommand
    {
        bool active;
        int dc;
        int hitCount;
        int dmg;
        IExpression dmgExpr;
        string pattern = @"--tohit ([0-9]+) ([0-9\+\-d/*]+) ?";
        public string Directions()
        {
            return pattern;
        }

        public string Final()
        {
            if (active)
                return string.Format("Hits: {0} Damage: {1}", hitCount, dmg);
            return null;
        }

        public void Init(string input)
        {
            active = Regex.IsMatch(input, pattern);
            if(active)
            {
                var match = Regex.Match(input, pattern);
                dc = int.Parse(match.Groups[1].Value);
                dmgExpr = ExpressionBuilder.Parse(match.Groups[2].Value);
            }
        }

        public string onEval(int result)
        {
            if (active)
            {
                if(dc <= result)
                {
                    hitCount++;
                    var hitDmg = dmgExpr.evaluate();
                    dmg += hitDmg;
                    return string.Format("Hit For {0}", hitDmg);
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
