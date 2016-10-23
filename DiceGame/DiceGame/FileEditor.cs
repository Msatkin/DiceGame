using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DiceGame
{
    class FileEditor
    {
        string fileData;

        public int LoadHighScore()
        {
            try
            {
                StreamReader read = new StreamReader("HighScore.txt");
                fileData = read.ReadLine();
                read.Close();
            }
            catch
            {
                StreamWriter write = new StreamWriter("HighScore.txt", false);
                write.WriteLine("250");
                write.Close();
                fileData = "250";
            }
            return ConvertHighScore();
        }
        public int ConvertHighScore()
        {
            try
            {
                return Convert.ToInt32(fileData);
            }
            catch
            {
                return 250;
            }
        }
        public void SaveNewHighScore(int score)
        {
                StreamWriter write = new StreamWriter("HighScore.txt", false);
                write.WriteLine(score);
                write.Close();
        }
    }
}
