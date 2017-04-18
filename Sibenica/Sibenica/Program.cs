using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sibenica
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxattempts = 20;
            string wordtoreveal = "slovo";
            Console.Write("Do you want to solve different word or phrase than the default one is? Y/N: ");
            string test = Console.ReadLine();
            if (test == "Y" || test == "y")
            {
                Console.WriteLine("All right than, type in your own expression:");
                wordtoreveal = Console.ReadLine();
                while (wordtoreveal == String.Empty)
                {
                    Console.WriteLine("Dummy, but not empty expression of course... Try again!");
                    wordtoreveal = Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("Ok, Proceeding with the defualt one.");
            }

            int lenghtoftherevealedword = wordtoreveal.Length;
            
            int attempts = maxattempts;
            int solvedletters = 0;
            string inputstring;
            char guessedcharacter;
            int correctlettercounter = 0;
            char[] unsolvedcharfield = new char[lenghtoftherevealedword];
            // fill char array with S T A R S
            for (int i = 0; i < lenghtoftherevealedword; i++)
            {
                unsolvedcharfield[i] = '*';
            }

            Console.WriteLine("Remember, also spaces and special characters can be part of the string!");
            
            bool gameunderway = true; // Game logic strats here.
            while (gameunderway == true)
            {
                Console.WriteLine("For now you revealed: ");
                Console.Write(unsolvedcharfield);
                Console.WriteLine(" in {0} attempts.", maxattempts - attempts);

                inputstring = Console.ReadLine();
                if (inputstring == String.Empty)
                    continue;
                guessedcharacter = inputstring[0];
                attempts--;

                for (int i = 0; i < lenghtoftherevealedword; i++)
                {
                    if ((wordtoreveal[i] == guessedcharacter) && (unsolvedcharfield[i] == '*'))
                    {
                        unsolvedcharfield[i] = wordtoreveal[i];
                        correctlettercounter++;
                    }
                }



                if (correctlettercounter == lenghtoftherevealedword)
                {
                    Console.WriteLine();
                    Console.WriteLine("Je to tam!");
                    Console.WriteLine();
                    gameunderway = false;
                }
                       

                if (attempts == 0)
                {
                    for (int i = 0; i < 10; i++ )
                        Console.WriteLine("Lama!!");
                    gameunderway = false;
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.WriteLine("The expression was: {0}", wordtoreveal);
            Console.ReadKey();

        }
    }
}
