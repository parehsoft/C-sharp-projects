using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogDeleterConsole
{
    class HelperImplementation : IHelper
    {
        public void DisplayHelp()
        {
            Console.WriteLine("\nUsage:");
            Console.WriteLine(" -> No parameters entered display this help.");
            Console.WriteLine();
            Console.WriteLine("This program is used with these mandatory paramters:");
            Console.WriteLine(" -p= (path) require directory path for deletion,");
            Console.WriteLine(" -e= (extension) require list of file extensions to be deleted, separated by comma (,).");
            Console.WriteLine("     If you wish to delete all files within specified folder, type * as argument for -e");
            Console.WriteLine();
            Console.WriteLine("Example: -p=G:\\Frqlog\\ -e=log,txt");
            Console.WriteLine("Example: -p=G:\\Frqlog\\ -e=*");
            Console.WriteLine();
            Console.WriteLine(" --NoLogging parameter specifies to disable standart output.");
        }
    }
}
