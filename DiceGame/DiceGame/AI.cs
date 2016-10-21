using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    class AI : Player
    {
        public AI(int place, int score)
        {
            this.name = "Player " + place;
            this.score = score;
        }
    }
}
