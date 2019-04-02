using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.AccountManagement;

namespace GetUserInfoFromAD
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                IHelper IH = ActiveDirectoryFactory.GetHelperDirectoryInterface();
                IH.DisplayHelp();
                return 0;
            }

            Application app = new Application();
            if (app.Run(args) ==  0)
            {
                return 0;
            }
            else
            {
                return 1;
            }

        } // Closing Main().
    }
}
