using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionParserCore.Models
{
    public class MultiExpression : IExpression
    {
        private IExpression value1;
        private IExpression value2;

        public MultiExpression(IExpression val1, IExpression val2)
        {
            value1 = val1;
            value2 = val2;
        }

        public int evaluate()
        {
           return value1.evaluate() * value2.evaluate();
        }

        public string toCalcString()
        {
            return string.Format("{0} * {1}", value1.toCalcString(), value2.toCalcString());
        }
    }
}
