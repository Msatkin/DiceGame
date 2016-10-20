using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    class GameController
    {
        Dice dice = new Dice();
        List<Player> playerList = new List<Player>();
        List<int> diceRollList = new List<int>();
        string[] diceArray = new string[6] { "D4", "D6", "D8", "D10", "D12", "D20" };
        string diceRollString;
        int width = 75;
        int height = 25;
        int maxScore = 200;
        int rowTwoStart = 16;
        int cursorX;
        int cursorY;
        bool isStarted = false;

        public GameController()
        {
            
        }
        public bool Begin()
        {
            FormatScreen();
            DisplayScreen();
            int numberOfHumanPlayers = GetNumberOfHumanPlayers();
            CreateHumanPlayers(numberOfHumanPlayers);
            int numberOfAIPlayers = GetNumberOfAIPlayers();
            CreateAIPlayers(numberOfAIPlayers);
            isStarted = true;
            bool gameOn = true;
            while (gameOn)
            {
                DisplayScreen();
                foreach (Player player in playerList)
                {
                    bool won = StartTurn(player);
                    if (won)
                    {
                        ShowWinner(player);
                        gameOn = false;
                    }
                }
            }
           return GetExit();
        }
        public bool StartTurn(Player player)
        {
            diceRollList = player.RollDice(dice);
            CreateDiceRollString();
            AddScore();
            DisplayScore();
            Console.Read();
            return (player.score >= maxScore);
        }
        public bool GetExit()
        {
            bool exit = false;
            while (!exit)
            {
                switch (GetInput())
                {
                    case "yes":
                        return true;

                    case "no":
                        return false;
                }
            }
            return false;
        }
        //Display Functions-------------------------------
        public void DisplayScore()
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                cursorX = 4;
                cursorY = 4 + (i * 2);
                SetCursor();
                Write(playerList[i].name + ": " + playerList[i].score);

            }
        }
        public void CreateDiceRollString()
        {
            diceRollString = "";
            for (int i = 0; i < diceRollList.Count; i++)
            {
                diceRollString += diceArray[i] + ": " + diceRollList[i] + "   ";
            }
            DisplayScreen();
        }
        public void ShowWinner(Player player)
        {

        }
        public void DisplayScreen()
        {
            Clear();
            FormatScreen();
            DrawBorder();
            if (isStarted)
            {
                DrawDiceRoll();
            }
            else
            {
                cursorX = 4;
                cursorY = 4;
            }
            SetCursor();
        }
        public void DrawDiceRoll()
        {
            cursorX = 4;
            cursorY = 18;
            SetCursor();
            Write(diceRollString);
        }
        public void DrawBorder()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.SetCursorPosition(j, i);
                    if (j == 0 || j == width -1 || i == 0 || i == height - 1)
                    {
                        Write("*");
                    }
                    if (isStarted && (i == rowTwoStart))
                    {
                        Write("*");
                    }
                }
                Write("\n");
            }
            Console.SetWindowPosition(0, 0);
        }
        public void FormatScreen()
        {
            Console.SetWindowSize(width, height);
            Console.SetWindowPosition(0, 0);
        }
        //Human and AI Creation Functions-----------------
        public int GetNumberOfHumanPlayers()
        {
            bool choiceMade = false;
            while (!choiceMade)
            {
                DisplayScreen();
                WriteLine("The maximum number of players is 5.\n*   How many human players will be playing?\n");
                Console.SetCursorPosition(4, 6);
                int numberOfPlayers = GetInputInt();
                if (numberOfPlayers >= 1 && numberOfPlayers <= 5)
                {
                    return numberOfPlayers;
                }
                Console.SetCursorPosition(5, 6);
            }
            return 0;
        }
        public void CreateHumanPlayers(int numberOfPlayers)
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                Player player = new Human(i + 1);
                playerList.Add(player);
            }
        }
        public void CreateAIPlayers(int numberOfAI)
        {
            int numberOfHumans = playerList.Count;
            for (int i = numberOfHumans - 1; i < numberOfAI + numberOfHumans; i++)
            {
                playerList.Add(new AI(i + 1));
            }
        }
        public int GetNumberOfAIPlayers()
        {
            bool choiceMade = false;
            while (!choiceMade)
            {
                DisplayScreen();
                WriteLine(String.Format("The maximum number of players is 5.\n*   There are {0} Human players \n*   How many AI players will be playing?", playerList.Count));
                Console.SetCursorPosition(4, 7);
                int numberOfAI = GetInputInt();
                if (numberOfAI >= 0 && numberOfAI <= 5 - playerList.Count)
                {
                    return numberOfAI;
                }
                Console.SetCursorPosition(5, 6);
            }
            return 0;
        }
        //Basic I/O Functions-----------------------------
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
        public void SetCursor()
        {
            Console.SetCursorPosition(cursorX, cursorY);
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
                DisplayScreen();
                Console.SetCursorPosition(4, 4);
                Write("Please enter a number.");
                Console.SetCursorPosition(4, 5);
                Write("Press Enter to Continue.");
                Console.ReadLine();
            }
            return -1;
        }
        public string GetInput()
        {
            DisplayScreen();
            Console.SetCursorPosition(4, 4);
            return Console.ReadLine();
        }
        public void Clear()
        {
            Console.Clear();
        }
    }
}