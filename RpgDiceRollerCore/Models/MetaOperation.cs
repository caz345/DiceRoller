using ExpressionParserCore.Models;
using RpgDiceRollerCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgDiceRollerCore.Models
{
    public class MetaOperation
    {
        public IExpression expression { get; set; }
        public CommandsBlah command { get; set; }
    }
}
