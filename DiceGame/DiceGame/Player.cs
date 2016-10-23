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
        public bool disqualified = false;
        public int score = 500;
        public Dice dice;
        public List<int> diceRollList;
        public List<List<int>> killList;
        public List<int> D4KillList = new List<int>();
        public List<int> D6KillList = new List<int>();
        public List<int> D8KillList = new List<int>();
        public List<int> D10KillList = new List<int>();
        public List<int> D12KillList = new List<int>();
        public List<int> D20KillList = new List<int>();
        public int turn = 1;
        
        public List<int> RollDice(Dice dice)
        {
            this.dice = dice;
            return diceRollList = new List<int> { dice.RollD4(), dice.RollD6(), dice.RollD8(), dice.RollD10(), dice.RollD12(), dice.RollD20() };
        }
        public List<List<int>> BuildKillList()
        {
            int killDice = turn;
            if  (killDice < 11 && killDice > 6)
            {
                killDice -= 6;
            }
            switch (killDice)
            {
                case 1:
                    D20KillList.Add(dice.RollD20());
                    break;

                case 2:
                    D12KillList.Add(dice.RollD12());
                    break;

                case 3:
                    D10KillList.Add(dice.RollD10());
                    break;

                case 4:
                    D8KillList.Add(dice.RollD8());
                    break;

                case 5:
                    D6KillList.Add(dice.RollD6());
                    break;

                case 6:
                    D4KillList.Add(dice.RollD4());
                    break;
            }
            killList = new List<List<int>>() { D4KillList, D6KillList, D8KillList, D10KillList, D12KillList, D20KillList };
            return killList;
        }
        public virtual bool CheckPromptRoll()
        {
            string input = Console.ReadLine().ToLower();
            if (input == "done")
            {
                return true;
            }
            return false;
        }
        public virtual void GetPause()
        {
            Console.ReadLine();
        }
    }
}
