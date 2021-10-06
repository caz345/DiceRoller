using ExpressionParserCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpressionParserCore
{
    public static class ExpressionBuilder
    {
        static List<char> infix = new List<char>{ '-', '+', '/', '*', 'd', 's'}; //Reverse of order of operations
        public static IExpression Parse(string input)
        {
            List<Tuple<int, char>> infixLocations = new List<Tuple<int, char>>();

            //Search for each operator
            for (var i = 0; i < input.Length; i++)
            {
                var character = input[i];
                //Check for parens
                if (character == '(')
                {
                    findEndParen(input, ref i);
                    character = input[i];
                }
                if (infix.Contains(character))
                    infixLocations.Add(new Tuple<int, char>(i, character));
            }
            foreach(var infixChar in infix)
            {
                var loc = infixLocations.FirstOrDefault(i => i.Item2 == infixChar);
                if (loc != null)
                    return ParseInfix(infixChar, input, loc.Item1);
            }
            if (input[0] == '(')
                return ParseParen(input);
            return new NumberExpression(int.Parse(input));
        }

        static bool skipParens(string input, ref int index)
        {
            var count = 0;
            var start = index;
            for(; index < input.Length; index++)
            {
                var character = input[index];
                if (character == '(')
                    count++;
                if (character == ')')
                    count--;
                if (count == 0)
                {
                    if(index != input.Length-1)
                    {
                        index++;
                        return true;
                    }
                    else if(start == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        static void findEndParen(string input, ref int index)
        {
            var count = 0;
            var start = index;
            for (; index < input.Length; index++)
            {
                var character = input[index];
                if (character == '(')
                    count++;
                if (character == ')')
                    count--;
                if (count == 0)
                {
                    return;
                }
            }
            return;
        }


        public static IExpression ParseInfix(char op, string input, int index)
        {
            switch (op)
            {
                case '+':
                    {
                        return ParseAdd(input, index);                        
                    }
                case '-':
                    {
                        return ParseSub(input, index);
                    }
                case '*':
                    {
                        return ParseMulti(input, index);
                    }
                case '/':
                    {
                        return ParseDiv(input, index);
                    }
                case 'd':
                    {
                        return ParseDie(input, index);
                    }
                case 's':
                    {
                        return ParseShadowrun(input, index);
                    }


            }
            throw new Exception("What Happened?");
        }

        public static IExpression ParseParen(string input)
        {
            input = input.Substring(1, input.Length - 2);
            return new ParenExpression(Parse(input));
        }

        public static IExpression ParseMulti(string input, int index)
        {
            var exp1 = input.Substring(0, index);

            var exp2 = input.Substring(index + 1, input.Length - (index + 1));

            return new MultiExpression(Parse(exp1), Parse(exp2));
        }

        public static IExpression ParseDiv(string input, int index)
        {
            var exp1 = input.Substring(0, index);

            var exp2 = input.Substring(index + 1, input.Length - (index + 1));

            return new DivExpression(Parse(exp1), Parse(exp2));
        }

        public static IExpression ParseAdd(string input, int index)
        {
            var exp1 = input.Substring(0, index);

            var exp2 = input.Substring(index+1, input.Length-(index+1));

            return new AddExpression(Parse(exp1), Parse(exp2));
        }

        public static IExpression ParseAdd(string[] input)
        {
            var exp1 = input[0];

            var exp2 = string.Join("+", input.Skip(1));

            return new AddExpression(Parse(exp1), Parse(exp2));
        }

        public static IExpression ParseSub(string input, int index)
        {
            var exp1 = input.Substring(0, index);

            var exp2 = input.Substring(index + 1, input.Length - (index + 1));
            
            if(exp1 == String.Empty)
            {
                exp1 = "0";
            }

            return new SubExpression(Parse(exp1), Parse(exp2));
        }

        public static IExpression ParseSub(string[] input)
        {
            var exp1 = input[0];

            var exp2 = string.Join("-", input.Skip(1));

            return new SubExpression(Parse(exp1), Parse(exp2));
        }

        public static IExpression ParseDie(string input, int index)
        {
            var exp1 = Parse(input.Substring(0, index));
            IExpression exp2;
            var string2 = input.Substring(index + 1, input.Length - (index + 1));
            exp2 = Parse(input.Substring(index + 1, input.Length - (index + 1)));
            return new DiceExpression(exp1, exp2);
        }

        public static IExpression ParseShadowrun(string input, int index)
        {
            var exp1 = Parse(input.Substring(0, index));
            IExpression exp2;
            var string2 = input.Substring(index + 1, input.Length - (index + 1));
            exp2 = Parse(input.Substring(index + 1, input.Length - (index + 1)));
            return new ShadowrunExpression(exp1, exp2);
        }


        public static IExpression ParseDie(string[] input)
        {
            var exp1 = Parse(input[0]);
            var exp2 = Parse(input[1]);

            return new DiceExpression(exp1, exp2);
        }

    }
}
