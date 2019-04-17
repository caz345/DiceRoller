using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionParserCore.Models
{
    public class ShadowrunExpression : IExpression
    {
        private static Random roller = new Random();
        private IExpression value1;
        private IExpression value2;

        private bool isCalc = false;
        private List<int> values = new List<int>();
        private int calc;
        private bool glitch;
        private bool criticalGlitch;

        public ShadowrunExpression(IExpression val1, IExpression val2)
        {
            value1 = val1;
            value2 = val2;
        }

        void calculate()
        {
            isCalc = true;
            values = new List<int>();
            var rolls = value1.evaluate();
            var faces = value2.evaluate();
            for (var i = 0; i < rolls; i++)
            {
                values.Add(roller.Next(1, faces + 1));
            }
            calc = values.Count(v => v > faces - 2);
            glitch = values.Count(v => v == 1) >= Math.Ceiling((decimal)rolls / 2);
            criticalGlitch = glitch && calc == 0;
        }

        public int evaluate()
        {
            calculate();
            return calc;

        }

        public string toCalcString()
        {
            if (!isCalc)
                calculate();

            var returnString = $"{value1.toCalcString()}s{value2.toCalcString()}{{{string.Join(", ", values)}}}";
            if (criticalGlitch) returnString += $" critical glitch";
            else if (glitch) returnString += "  glitch";
            return returnString;
        }
    }
}
