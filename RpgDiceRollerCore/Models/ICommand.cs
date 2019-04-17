using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgDiceRollerCore.Models
{
    public interface ICommand
    {
        string Directions();        

        void Init(string input);

        string onEval(int result);

        string Final();

        string remove(string input);
    }
}
