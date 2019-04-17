using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionParserCore.Models
{
    public class DiceExpression : IExpression
    {
        private static Random roller = new Random();
        private IExpression value1;
        private IExpression value2;

        private bool isCalc = false;
        private List<int> values = new List<int>();
        private int calc;

        public DiceExpression(IExpression val1, IExpression val2)
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
            calc = values.Sum();

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

            return string.Format("{1}d{2}{{{0}}}", string.Join(" + ", values), value1.toCalcString(), value2.toCalcString());
        }
    }
}
