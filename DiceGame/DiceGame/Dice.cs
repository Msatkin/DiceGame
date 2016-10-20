using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    
    class Dice
    {
        Random random = new Random();

        public int RollD4()
        {
            return random.Next(1, 4);
        }

        public int RollD6()
        {
            return random.Next(1, 6);
        }

        public int RollD8()
        {
            return random.Next(1, 8);
        }

        public int RollD10()
        {
            return random.Next(1, 10);
        }

        public int RollD12()
        {
            return random.Next(1, 12);
        }
        public int RollD20()
        {
            return random.Next(1, 20);
        }
    }
}
