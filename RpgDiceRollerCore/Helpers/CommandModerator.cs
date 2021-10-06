using RpgDiceRollerCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using RpgDiceRollerCore.Commands;

namespace RpgDiceRollerCore.Helpers
{
    public class CommandModerator
    {
        List<ICommand> commands = new List<ICommand>();

        public CommandModerator()
        {
            commands.Add(new SumCommand());
            commands.Add(new DcCommand());
            commands.Add(new AverageCommand());
            commands.Add(new ToHitCommand());
            commands.Add(new CthuluCommand());
        }

        public void Init(string input)
        {
            foreach (var command in commands)
            {
                command.Init(input);
            }
        }

        public string Help()
        {
            var resultString = string.Join("; ", commands.Select(c => c.Directions()).Where(r => r != null));
            return resultString != string.Empty ? resultString : null;

        }

        public string RemoveCommands(string input)
        {
            foreach(var command in commands)
            {
                input = command.remove(input);
            }
            return input;
        }

        public string EvalCommands(int result)
        {
            var resultString = string.Join("; ", commands.Select(c => c.onEval(result)).Where(r => r != null));
            return resultString != string.Empty ? resultString : null;
        }

        public string FinalOutput()
        {
            var resultString = string.Join("; ", commands.Select(c => c.Final()).Where(r => r != null));
            return resultString != string.Empty ? resultString : null;

        }
    }
}
