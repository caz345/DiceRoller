using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionParserCore.Models
{
    public interface IExpression
    {
        int evaluate();
        string toCalcString();
    }
}
