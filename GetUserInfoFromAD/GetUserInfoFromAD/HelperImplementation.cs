using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace GetUserInfoFromAD
{
    class HelperImplementation : IHelper
    {
        public void DisplayHelp()
        {
            Console.WriteLine("\nUsage:");
            Console.WriteLine(" -> No parameters display this help.");
            Console.WriteLine(" -> -u=USER_NAME -> It shows which groups this user bellongs to.");
            Console.WriteLine(" -> -u=USER_NAME -g=GROUP_NAME -> It checks if the given user belongs to given group. Returns true, or false.");
            Console.WriteLine(" -> -u=USER_NAME -p=PASSWORD -> Checks if given user has given password. Returns true, or false.");
            Console.WriteLine("Aforementioned parameters can be also combined with parameter:");
            Console.WriteLine(" -> -d=DNS_DOMAIN_NAME,USER,PASSWORD to connect to the foreign, domain.");
        }

        public void PrintArrayListsContent(ArrayList myAL)
        {
            if (myAL.Count != 0)
            {
                foreach (Object o in myAL)
                { Console.WriteLine(o); }
            }
        }
    
    } //class
}
