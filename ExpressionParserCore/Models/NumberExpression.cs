using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionParserCore.Models
{
    public class NumberExpression : IExpression
    {
        private int value { get; set; }
        public NumberExpression(int val)
        {
            value = val;
        }

        public int evaluate()
        {
            return value;
        }

        public string toCalcString()
        {
            return value.ToString();
        }

    }
}
