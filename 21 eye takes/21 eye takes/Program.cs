using System;

namespace _21_eye_takes
{
    class Program
    {
        static void Main(string[] args)
        {
            string gameStarter = String.Empty;
            Console.WriteLine("Copyright 2016 by Rehorcik Game Solutions.");
            Console.WriteLine("You're about to play 21 eye takes (21 oko berie :D) for 3 players!");
            Console.WriteLine("Game is playable also for 2 players when the 3rd player never draws any card.");

            do
            {
                Console.Write("Please enter licence key XXXXX-XXXXX-XXXXX-XXXXX: ");
                gameStarter = Console.ReadLine();
                if (gameStarter != String.Empty)
                    Console.WriteLine("Wrong key! Please contact manufacturer to purchase a genuine key!");
            } while (gameStarter != String.Empty);

            Console.WriteLine(":D No licence key is the best licence key of course!");
            Console.WriteLine("\n");

            Random rand = new Random();
            int firstPlayerScore, secondPlayerScore, thirdPlayerScore;
            int rand1, rand2, rand3;
            string inputString;
            char inputChar1stPlayer = 'n';
            char inputChar2ndPlayer = 'n';
            char inputChar3rdPlayer = 'n';
            char playagain = 'n';
            bool firstPlayerPlays, secondPlayerPlays, thirdPlayerPlays;

            do
            {
                firstPlayerScore = 0;  firstPlayerPlays = true;
                secondPlayerScore = 0; secondPlayerPlays = true;
                thirdPlayerScore = 0;  thirdPlayerPlays = true;

                while (true)
                {
                    Console.WriteLine("1st player     2nd player     3rd player");
                    rand1 = rand.Next(2, 12);
                    rand2 = rand.Next(2, 12);
                    rand3 = rand.Next(2, 12);
                    Console.WriteLine("{0}              {1}              {2}", firstPlayerScore, secondPlayerScore, thirdPlayerScore);

                    if (firstPlayerPlays == true)
                    {
                        do
                        {
                            Console.Write("1st player, do you want to draw an another card? Y/N : ");
                            inputString = Console.ReadLine().ToLower();
                            if (inputString == String.Empty)
                                continue;
                            inputChar1stPlayer = inputString[0];
                        } while (inputString == string.Empty);

                        if (inputChar1stPlayer == 'y')
                            firstPlayerScore += rand1;
                        else
                            Console.WriteLine("1st player chosen not to draw a card!");
                    }

                    if (secondPlayerPlays == true)
                    {
                        do
                        {
                            Console.Write("2nd player, do you want to draw an another card? Y/N : ");
                            inputString = Console.ReadLine().ToLower();
                            if (inputString == String.Empty)
                                continue;
                            inputChar2ndPlayer = inputString[0];
                        } while (inputString == string.Empty);

                        if (inputChar2ndPlayer == 'y')
                            secondPlayerScore += rand2;
                        else
                            Console.WriteLine("2nd player chosen not to draw a card!");
                    }

                    if (thirdPlayerPlays == true)
                    {
                        do
                        {
                            Console.Write("3rd player, do you want to draw an another card? Y/N : ");
                            inputString = Console.ReadLine().ToLower();
                            if (inputString == String.Empty)
                                continue;
                            inputChar3rdPlayer = inputString[0];
                        } while (inputString == string.Empty);

                        if (inputChar3rdPlayer == 'y')
                            thirdPlayerScore += rand3;
                        else
                            Console.WriteLine("3rd player chosen not to draw a card!");
                    }

                    Console.WriteLine();
                    if (inputChar1stPlayer != 'y' && inputChar2ndPlayer != 'y' && inputChar3rdPlayer != 'y')
                    {
                      Console.WriteLine("This is the end of the round, proceeding to results...");
                      break;
                    }

                    if (firstPlayerScore > 21)
                    {
                        Console.WriteLine("1st player's score is equal or higher than 22 and is out!");
                        firstPlayerPlays = false;
                        inputChar1stPlayer = 'n';
                        firstPlayerScore = -1;
                    }

                    if (secondPlayerScore > 21)
                    {
                        Console.WriteLine("2nd player's score is equal or higher than 22 and is out!");
                        secondPlayerPlays = false;
                        inputChar2ndPlayer = 'n';
                        secondPlayerScore = -1;
                    }

                    if (thirdPlayerScore > 21)
                    {
                        Console.WriteLine("3rd player's score is equal or higher than 22 and is out!");
                        thirdPlayerPlays = false;
                        inputChar3rdPlayer = 'n';
                        thirdPlayerScore = -1;
                    }

                    if (firstPlayerPlays == false && secondPlayerPlays == false && thirdPlayerPlays == false)
                        break;
                }
            
                Console.WriteLine("Final score: p1: {0}, p2: {1}, p3: {2}.", firstPlayerScore, secondPlayerScore, thirdPlayerScore);

                if (firstPlayerScore == -1 && secondPlayerScore == -1 && thirdPlayerScore == -1)
                    Console.WriteLine("Your're all out! Money's mine HA-HA-HA!");
                else if (firstPlayerScore == secondPlayerScore && secondPlayerScore == thirdPlayerScore)
                    Console.WriteLine("WOW! All is equal! Nobody wins...");
                else if (firstPlayerScore > secondPlayerScore)
                {
                    if (firstPlayerScore > thirdPlayerScore)
                        Console.WriteLine("1st player wins! Long live player 1!");
                    else
                        Console.WriteLine("3rd player wins! Long live player 3!");
                }
                else
                {
                    if (secondPlayerScore > thirdPlayerScore)
                        Console.WriteLine("2nd player wins! Long live player 2!");
                    else
                        Console.WriteLine("3rd player wins! Long live player 3!");
                }

                Console.WriteLine();
            
                do
                {
                    Console.Write("Do you wanna play again? Y/N: ");
                    inputString = Console.ReadLine().ToLower();
                    if (inputString == String.Empty)
                        continue;
                    playagain = inputString[0];
                } while (inputString == string.Empty);

            } while (playagain != 'n');
        }
    }
}