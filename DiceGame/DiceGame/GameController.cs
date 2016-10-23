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
        List<Player> playersLeft = new List<Player>();
        List<int> diceRollList = new List<int>();
        string[] diceArray = new string[6] { "D4", "D6", "D8", "D10", "D12", "D20" };
        string diceRollString;
        int allTimeHighScore;
        int width = 75;
        int height = 25;
        int maxScore = 250;
        int rowTwoStart = 16;
        int cursorX;
        int cursorY;
        int progressBarLength = 50;
        int progressBarStart = 21;
        int progressBarEnd = 71;
        bool playerDone = false;
        bool isStarted = false;
        bool gameOn = true;
        bool playerTurnDone = false;
        bool turnDone = false;
        Player currentPlayer;
        Player winningPlayer;
        FileEditor editFile = new FileEditor();
        Random random = new Random();
        public List<int> D4KillList = new List<int>();
        public List<int> D6KillList = new List<int>();
        public List<int> D8KillList = new List<int>();
        public List<int> D10KillList = new List<int>();
        public List<int> D12KillList = new List<int>();
        public List<int> D20KillList = new List<int>();
        public List<List<int>> killList;

        public bool Begin()
        {
            allTimeHighScore = editFile.LoadHighScore();
            CreateKillList();
            FormatScreen();
            DisplayScreen();
            CreateHumanPlayers(GetNumberOfHumanPlayers());
            CreateAIPlayers(GetNumberOfAIPlayers());
            CreatePlayersLeft();
            isStarted = true;
            while (gameOn)
            {
                KeepGameRunning();
            }
            Console.ReadLine();
            return GetExit();
        }
        public void KeepGameRunning()
        {
            GetCurrentPlayer();
            RunCurrentPlayerTurn();
            if (!playerTurnDone)
            {
                while (!playerDone)
                {
                    playerDone = StartTurn();
                    currentPlayer.turn++;
                }
                playersLeft.Remove(currentPlayer);
                playerDone = false;
            }
        }
        public void RunCurrentPlayerTurn()
        {
            while (!playerDone)
            {
                playerDone = StartTurn();
                currentPlayer.turn++;
            }
            playersLeft.Remove(currentPlayer);
            playerDone = false;
            playerTurnDone = true;
            ClearKillList();
        }
        public bool StartTurn()
        {
            PromptRoll();
            if (turnDone)
            {
                turnDone = false;
                return true;
            }
            else
            {
                return ContinueTurn();
            }
        }
        public bool ContinueTurn()
        {
            diceRollList = currentPlayer.RollDice(dice);
            if (!CompareToKillList())
            {
                DisplayTurnOverScreen();
                return true;
            }
            else
            {
                CreateDiceRollString();
                AddScore();
                DisplayScreen();
                cursorX = 4;
                cursorY = 19;
                SetCursor();
                currentPlayer.GetPause();
            }
            killList = currentPlayer.BuildKillList();
            return (currentPlayer.score <= 0);
        }
        public void CreatePlayersLeft()
        {
            foreach (Player player in playerList)
            {
                playersLeft.Add(player);
            }
        }
        public void CreateKillList()
        {
            killList = new List<List<int>>() { D4KillList, D6KillList, D8KillList, D10KillList, D12KillList, D20KillList };
        }
        public void ClearKillList()
        {
            D4KillList.Clear();
            killList[0] = D4KillList;
            D6KillList.Clear();
            killList[1] = D6KillList;
            D8KillList.Clear();
            killList[2] = D8KillList;
            D10KillList.Clear();
            killList[3] = D10KillList;
            D12KillList.Clear();
            killList[4] = D12KillList;
            D20KillList.Clear();
            killList[5] = D20KillList;
        }
        public void GetCurrentPlayer()
        {
            if (playersLeft.Count == 0)
            {
                FindWinner();
                ShowWinner();
                playerDone = true;
                gameOn = false;
            }
            else
            {
            currentPlayer = playersLeft[random.Next(0, playersLeft.Count - 1)];
            }
        }
        public void FindWinner()
        {
            int highScore = 501;
            foreach (Player player in playerList)
            {
                if (player.score < highScore && !player.disqualified)
                {
                    highScore = player.score;
                    winningPlayer = player;
                    if (highScore < allTimeHighScore)
                    {
                        editFile.SaveNewHighScore(highScore);
                    }
                }
            }
        }
        public bool CompareToKillList()
        {
            for (int i = 0; i < killList.Count; i++)
            {
                foreach (int roll in killList[i])
                {
                    if (roll == diceRollList[i])
                    {
                        currentPlayer.disqualified = true;
                        return false;
                    }
                }
            }
            return true;
        }
        public void AddScore()
        {
            foreach (int number in diceRollList)
            {
                currentPlayer.score -= number;
                if (currentPlayer.score < 0)
                {
                    currentPlayer.score = 0;
                }
            }
        }
        public void CreateDiceRollString()
        {
            diceRollString = "";
            for (int i = 0; i < diceRollList.Count; i++)
            {
                diceRollString += diceArray[i] + ": " + diceRollList[i] + "   ";
            }
        }
        public bool GetExit()
        {
            bool exit = false;
            while (!exit)
            {
                Clear();
                WriteLine("Exit? (yes/no)");
                switch (Console.ReadLine())
                {
                    case "yes":
                        return true;

                    case "no":
                        return false;
                }
            }
            return false;
        }
        public void PromptRoll()
        {
            diceRollList.Clear();
            diceRollString = currentPlayer.name + ", type 'roll' to roll the dice \n*   or type 'done' to finish your turn.";
            DisplayScreen();
            cursorX = 4;
            cursorY = 20;
            SetCursor();
            if (currentPlayer.CheckPromptRoll())
            {
                turnDone = true;
                DisplayTurnOverScreen();
            }
        }
        //Display Functions-------------------------------
        public void DisplayTurnOverScreen()
        {
            diceRollList.Clear();
            diceRollString = currentPlayer.name + ", your turn is over!\n*    You got a score of " + currentPlayer.score;
            DisplayScreen();
            cursorX = 4;
            cursorY = 20;
            SetCursor();
            ClearKillList();
            currentPlayer.GetPause();
        }
        public void DrawScore()
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                cursorX = 4;
                cursorY = 4 + (i * 2);
                SetCursor();
                if (playerList[i].disqualified)
                {
                    Write(playerList[i].name + ": ---");
                }
                else
                {
                    Write(playerList[i].name + ": " + playerList[i].score);
                    BuildProgressBar(i);
                }
                

            }
        }
        public void BuildProgressBar(int playerIndex)
        {
            cursorX = progressBarStart;
            SetCursor();
            Write("[");
            for (int i = 0; i < maxScore; i += maxScore/progressBarLength)
            {
                if (playerList[playerIndex].score > i)
                {
                    Write("|");
                }
            }
            cursorX = progressBarEnd;
            SetCursor();
            Write("]");
        }
        public void ShowWinner()
        {
            Clear();
            FormatScreen();
            DrawBorder();
            cursorX = 4;
            cursorY = 4;
            SetCursor();
            Write(winningPlayer.name + " is the winner!");
            Console.ReadLine();
            playerTurnDone = false;
        }
        public void DisplayScreen()
        {
            Clear();
            FormatScreen();
            DrawBorder();
            if (isStarted)
            {
                DrawDiceRoll();
                DrawScore();
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
                Player player = new Human(i + 1, maxScore);
                playerList.Add(player);
            }
        }
        public void CreateAIPlayers(int numberOfAI)
        {
            int numberOfHumans = playerList.Count;
            for (int i = numberOfHumans; i < numberOfAI + numberOfHumans; i++)
            {
                playerList.Add(new AI(i + 1, maxScore));
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
            Console.SetCursorPosition(cursorX, cursorY);
            return Console.ReadLine();
        }
        public void Clear()
        {
            Console.Clear();
        }
    }
}