using ExpressionParserCore;
using ExpressionParserCore.Models;
using RpgDiceRollerCore.Models;
using System.Collections.Generic;
using System.Linq;

namespace RpgDiceRollerCore.Helpers
{
    public static class ParserHelper
    {
        public static int GetRepeatTimes(ref string input)
        {
            var parsed = input.Split('x');
            int times = 1;
            if (parsed.Count() > 1)
            {
                times = short.Parse(parsed[0]);
                input = parsed[1];
            }
            return times;
        }

        public static List<IExpression> GetExpressionList(string input)
        {
            var times = ParserHelper.GetRepeatTimes(ref input);

            var initExpressions = new List<IExpression>();
            var expressionStrings = input.Split(';');
            foreach (var expressionString in expressionStrings)
            {
                var exp = ExpressionBuilder.Parse(expressionString);
                initExpressions.Add(exp);
            }

            //Get Repeat
            var expressionList = new List<IExpression>();
            for (var i = 0; i < times; i++)
            {
                foreach (var exp in initExpressions)
                {
                    expressionList.Add(exp);
                }
            }
            return expressionList;
        }

        public static string GetOutString(IExpression expression, CommandModerator commandMod)
        {
            var result = expression.evaluate();
            var commandString = commandMod.EvalCommands(result);
            if(commandString != null)
                return string.Format("{0} : {1}; {2}", result, expression.toCalcString(), commandString);
            return string.Format("{0} : {1}", result, expression.toCalcString());

        }

        public static string GetFinalOutString(List<MetaOperation> operations)
        {
            var finalOutstring = new List<string>();

            foreach (var op in operations)
            {
                if (op.command.any) finalOutstring.Add(op.command.calcFinal());
            }
            if (finalOutstring.Any()) return string.Join("; ", finalOutstring);
            else return null;
        }


        public static List<MetaOperation> GetOperations(string input, CommandsBlah commands)
        {
            var operations = new List<MetaOperation>();
            var parsed = input.Split(';');
            foreach (var expr in parsed)
            {
                operations.Add(new MetaOperation
                {
                    expression = ExpressionBuilder.Parse(expr),
                    command = new CommandsBlah(commands)
                });
            }
            return operations;
        }

    }
}
