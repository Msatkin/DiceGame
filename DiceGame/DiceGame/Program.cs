using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    class Program
    {
        static void Main(string[] args)
        {
            GameController gamecontroller = new GameController();
            gamecontroller.Begin();
        }
    }
}
