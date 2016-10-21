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
            bool exit = false;
            GameController gamecontroller;
            while (!exit)
            {
                gamecontroller = new GameController();
                exit = gamecontroller.Begin();
                Console.Write("");
                gamecontroller = null;
            }
        }
    }
}
