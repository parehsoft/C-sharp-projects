using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace GetUserInfoFromAD
{
    class Application
    {
        public int Run(string[] args)
        {
            const char splitter = '=';

            Dictionary<string, string> paramDictionary = new Dictionary<string, string>();
            IParameterParser IPP = ActiveDirectoryFactory.GetParameterParserInterface();
            paramDictionary = IPP.ParseParameters(args, splitter);
            
            bool domainFlag = false;
            string domainString = string.Empty;

            bool userFlag = false;
            string userString = string.Empty;

            bool groupFlag = false;
            string groupString = string.Empty;

            bool passwordFlag = false;
            string passwordString = string.Empty;

            foreach (KeyValuePair<string, string> keyValuePair in paramDictionary) 
            {
                switch (keyValuePair.Key)
                {
                    case "-h":
                        IHelper IH = ActiveDirectoryFactory.GetHelperDirectoryInterface();
                        Console.WriteLine("Help:");
                        IH.DisplayHelp();
                         return 0;
                        break;

                    case "-d":
                        domainString = keyValuePair.Value;
                        if (domainString == string.Empty)
                        {
                            Console.WriteLine("Connection string for -d parameter can not be empty!");
                            return 1;
                        }
                        domainFlag = true;
                       break;

                    case "-u":
                        userString = keyValuePair.Value;
                        if (userString == string.Empty)
                        {
                            Console.WriteLine("User string for -u parameter can not be empty!");
                            return 1;
                        }
                        userFlag = true;
                       break;

                    case "-g":
                        groupString = keyValuePair.Value;
                        if (groupString == string.Empty)
                        {
                            Console.WriteLine("Group string for -g parameter can not be empty!");
                            return 1;
                        }
                        groupFlag = true;
                       break;

                    case "-p":
                        passwordString = keyValuePair.Value;
                        if (passwordString == string.Empty)
                        {
                            Console.WriteLine("Password string for -p parameter can not be empty!");
                            return 1;
                        }
                        passwordFlag = true;
                       break;

                    default:
                        Console.WriteLine("Wrong parameters insertion.");
                         return 1;
                        break;
                }
            }

            if ((groupFlag == true) && (userFlag == false))
            {
                Console.WriteLine("User was not specified! Use -g with combination of -u .");
                return 1;
            }

            if ((passwordFlag == true) && (userFlag == false))
            {
                Console.WriteLine("User was not specified! Use -p with combination of -u .");
                return 1;
            }
            
            if ((groupFlag == true) && (passwordFlag == true))
            {
                Console.WriteLine("Wrong parameter combination, -g and -p can't be used together, exiting!");
                return 1;
            }

            IActiveDirectory IAD;
            IHelper iHelper = ActiveDirectoryFactory.GetHelperDirectoryInterface();

            if (domainFlag == true)
            {
                // Use external domain.
                IAD = ActiveDirectoryFactory.GetActiveDirectoryInterface(domainString);
            }
            else
            {
                // Use current domain.
                IAD = ActiveDirectoryFactory.GetActiveDirectoryInterface();
            }

            if (userFlag == true)
            {
                if (groupFlag == true)
                {
                    // Prcoess -u + -g paramter compbination.
                    if (IAD.IsUserWithinGroup(userString, groupString) == true)
                    {
                        Console.WriteLine("true");
                    }
                    else
                    {
                        Console.WriteLine("false");
                    }
                }
                else if (passwordFlag == true)
                {
                    // Prcoess -u + -p paramter compbination.
                    if (IAD.IsUsersPassword(userString, passwordString) == true)
                    {
                        Console.WriteLine("true");
                    }
                    else
                    {
                        Console.WriteLine("false");
                    }
                }
                else
                {
                    // Process -u parameter only.
                    string domainName = IAD.ReturnDomainName();
                    ArrayList allOfGroups = new ArrayList();
                    allOfGroups = IAD.GetUsersGroups(userString);
                    if (allOfGroups.Count == 0)
                    {
                        Console.WriteLine("No groups found for user \"{0}\" within {1} .", userString, domainName);
                    }
                    else
                    {
                        Console.WriteLine("User \"{0}\", within " + domainName + ", is member of the following groups:\n", userString);
                        iHelper.PrintArrayListsContent(allOfGroups);
                    }
                }
            }

            return 0; // Run function end. All is OK.
        }
    }
}
