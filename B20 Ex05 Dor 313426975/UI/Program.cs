using System;
using System.Collections.Generic;
using System.Text;

namespace UI
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            SystemManager manager = UI.LoginAndSetGame();

            manager.PlayMatchGame();

            System.Console.WriteLine("Please press 'Enter' to exit...");
            System.Console.ReadLine();
        }
    }
}
