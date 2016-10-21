using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    class Human : Player
    {
        public Human(int place, int score)
        {
            this.name = "Player " + place;
            this.score = score;
        }
    }
}
