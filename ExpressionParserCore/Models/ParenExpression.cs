using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionParserCore.Models
{
    public class ParenExpression : IExpression
    {
        private IExpression value;
        public ParenExpression(IExpression val)
        {
            value = val;
        }

        public int evaluate()
        {
            return value.evaluate();
        }

        public string toCalcString()
        {
            return string.Format("({0})", value.toCalcString());
        }
    }
}
