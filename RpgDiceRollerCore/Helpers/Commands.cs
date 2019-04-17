using ExpressionParserCore.Models;
using RpgDiceRollerCore.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RpgDiceRollerCore.Helpers
{
    public class CommandsBlah
    {
        public bool any { get; private set; }

        bool isDC = false;
        int DC = 0;
        int DCCount = 0;

        bool isSum = false;
        int sum = 0;

        bool isAvg;
        int count = 0;

        bool isToHit = false;
        IExpression toHitExp;
        int toHitSum = 0;

        public CommandsBlah(ref string input)
        {
            input = ParseCommands(input);
        }

        private CommandsBlah() { }

        public CommandsBlah(CommandsBlah ori)
        {
            this.any = ori.any;
            this.isAvg = ori.isAvg;
            this.isDC = ori.isDC;
            this.DC = ori.DC;
            this.isSum = ori.isSum;
            this.isToHit = ori.isToHit;
            this.toHitExp = ori.toHitExp;
        }
        public string ParseCommands(string input)
        {
            var inputs = input.Split('|');
            if (inputs.Length == 1)
                return input;
            if (inputs[0].Contains("--save:"))
            {
                var name = Regex.Replace(inputs[0], @".*--save:([a-z0-9]+).*", @"$1");
                SaveQuery.Save(name, inputs[1]);
            }
            if(inputs[0].Contains("--delete:"))
            {
                var name = Regex.Replace(inputs[0], @".*--delete:([a-z0-9]+).*", @"$1");
                SaveQuery.Delete(name);

            }
            if (inputs[0].Contains("--savefile:"))
            {
                var name = Regex.Replace(inputs[0], @".*--savefile:([a-z0-9]+).*", @"$1");
                SaveQuery.SaveToFile(name);
            }
            if(inputs[0].Contains("--loadfile:"))
            {
                var name = Regex.Replace(inputs[0], @".*--loadfile:([a-z0-9]+).*", @"$1");
                var commands = SaveQuery.LoadFile(name);
                foreach (var command in commands)
                {
                    this.ParseCommands(command);
                }
            }
            if (inputs[0].Contains("--load:"))
            {
                var name = Regex.Replace(inputs[0], @".*--load:([a-z0-9]+).*", @"$1");
                inputs[1] = SaveQuery.GetQuery(name);                
            }
            if (inputs[0].Contains("--dc:"))
            {
                isDC = true;
                any = true;
                DC = int.Parse(Regex.Replace(inputs[0], @".*--dc:([0-9]+).*", @"$1"));
            }
            if(inputs[0].Contains("--tohit"))
            {
                isToHit = true;
                isDC = true;
                any = true;
                DC = int.Parse(Regex.Replace(inputs[0], @".*--tohit:([0-9]+).*", @"$1"));
                var exprs = ParserHelper.GetOperations(Regex.Replace(inputs[1],@"[0-9]+x(.*)", @"$1"), this);
                inputs[1] = inputs[1].Split(';')[0];
                toHitExp = exprs[1].expression;
            }
            if (inputs[0].Contains("--sum"))
            {
                isSum = true;
                any = true;
            }
            if(inputs[0].Contains("--avg"))
            {
                isSum = true;
                isAvg = true;
                any = true;
            }
            return inputs[1];            
        }
        public string CalcCommands(int val)
        {
            var retstring = new List<string>();
            if (isDC)
            {
                retstring.Add(calcDC(val));
            }
            if(isToHit)
            {
                retstring.Add(calcToHit(val));
            }
            if(isSum)
            {
                calcSum(val);
            }
            if (isAvg)
            {
                calcAvg(val);
            }

            return string.Join("; ", retstring);
        }

        string calcToHit(int val)
        {
            if(val >= DC)
            {
                var value = toHitExp.evaluate();
                toHitSum += value;
                return string.Format("Damage {0} = {1}", value, toHitExp.toCalcString());
            }
            return "No Damage";
        }

        string calcSum(int val)
        {
            sum += val;
            return string.Empty;
        }

        string calcDC(int val)
        {
            if(val >= DC)
            {
                DCCount++;
                return "Hit";
            }
            return "Miss";
        }

        string calcAvg(int val)
        {
            count++;
            return string.Empty;
        }

        public string calcFinal()
        {
            var retstring = new List<string>();

            if (isDC)
            {
                retstring.Add(string.Format("{0} Hits", DCCount));
            }
            if (isToHit)
            {
                retstring.Add(string.Format("{0} Damage", toHitSum));
            }
            if (isSum)
            {
                retstring.Add(string.Format("{0} Total", sum));
            }
            if (isAvg)
            {
                retstring.Add(string.Format("{0} Average", sum/count));
            }

            return string.Join("| ", retstring);


        }

    }

}
