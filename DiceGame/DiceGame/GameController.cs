using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    class GameController
    {
        List<Player> playerList;

        public GameController()
        {
            
        }
        public void Begin()
        {
            bool exit = false;
            while(!exit)
            {
                int numberOfHumanPlayers = GetNumberOfHumanPlayers();
                CreateHumanPlayers(numberOfHumanPlayers);
                int numberOfAIPlayers = GetNumberOfAIPlayers();
                CreateAIPlayers(numberOfAIPlayers);

            }
        }









        //Human and AI Creation Functions-----------------
        public int GetNumberOfHumanPlayers()
        {
            bool choiceMade = false;
            while (!choiceMade)
            {
                Clear();
                WriteLine("The maximum number of players is 5. How many human players will be playing?");
                int numberOfPlayers = GetInputInt();
                if (numberOfPlayers >= 1 && numberOfPlayers <= 5)
                {
                    return numberOfPlayers;
                }
            }
            return 0;
        }
        public void CreateHumanPlayers(int numberOfPlayers)
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                playerList[i] = new Human(i + 1);
            }
        }
        public void CreateAIPlayers(int numberOfAI)
        {
            int numberOfHumans = playerList.Count;
            for (int i = numberOfHumans - 1; i < numberOfAI + numberOfHumans; i++)
            {
                playerList[i] = new AI(i + 1);
            }
        }
        public int GetNumberOfAIPlayers()
        {
            bool choiceMade = false;
            while (!choiceMade)
            {
                Clear();
                WriteLine(String.Format("The maximum number of players is 5. There are {0} Human players \nHow many AI players will be playing?", playerList.Count));
                int numberOfAI = GetInputInt();
                if (numberOfAI >= 1 && numberOfAI <= 5 - playerList.Count)
                {
                    return numberOfAI;
                }
            }
            return 0;
        }

        //Basic I/O Functions-----------------------------
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
        public void Write(string message)
        {
            Console.Write(message);
        }
        public int GetInputInt()
        {
            string input = Console.ReadLine();
            try
            {
                int outputInt = Convert.ToInt32(input);
                return outputInt;
            }
            catch
            {
                Clear();
                WriteLine("Please enter a number.");
                Console.ReadLine();
            }
            return -1;
        }
        public string GetInput()
        {
            return Console.ReadLine();
        }
        public void Clear()
        {
            Console.Clear();
        }
    }
}