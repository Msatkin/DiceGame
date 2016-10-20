using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    class Player
    {
        public string name;
        public int score;
        public Dice dice;
        public List<int> diceRollList;

        public List<int> RollDice(Dice dice)
        {
            this.dice = dice;
            return diceRollList = new List<int> { dice.RollD4(), dice.RollD6(), dice.RollD8(), dice.RollD10(), dice.RollD12(), dice.RollD20() };
        }
    }
}
