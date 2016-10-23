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
            this.name = "AI Player " + place;
            this.score = score;
        }
        public override bool CheckPromptRoll()
        {
            GetPause();
            if (score <= 140)
            {
                if (dice.RollD2() == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public override void GetPause()
        {
            System.Threading.Thread.Sleep(750);
        }
    }
}
