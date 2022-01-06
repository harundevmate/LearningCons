using LearningCons.Oracle;
using System;

namespace LearningCons
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //new DBOracle().GetActiveSub("6285725000177");
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        private static bool MainMenu()
        {
            Console.Write("\r\nInput Msisdn: ");
            //Console.Write(Console.ReadLine());
            DBOracle db = new DBOracle();
            db.CopySubsToUat(Console.ReadLine());
            return true;
        }

    }
}
