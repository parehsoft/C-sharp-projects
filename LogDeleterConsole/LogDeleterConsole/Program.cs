using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogDeleterConsole
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                IHelper IH = LogDeleterFactory.GetHelperImplementation();
                IH.DisplayHelp();
                Console.ReadKey();
                return 0; // OK, help was displayed, finito.
            }

            Application app = new Application();

            if (app.Run(args) == 0)
            {
                Console.ReadKey();
                return 0;
            }
            else
            {
                Console.ReadKey();
                return 1;
            }
        }
    }
}
