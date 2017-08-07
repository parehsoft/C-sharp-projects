using System;
using System.Collections.Generic;
using System.Text;

namespace Defend_the_Motherland_
{
    class Program
    {
        static void PrintHeadder()
        {
            Console.WriteLine("Copyright 2016 by Rehorčík Game Solutions");
            Console.WriteLine("Defend the Motherland! pre-ALPHA");
            for (int i = 0; i < 11; i++)
            {
                Console.Write(" CAUTION: ALPHA version!");
            }
            for (int i = 0; i < 3; i++)
                Console.WriteLine();
        }

        static char ReadtheKey () // RETURNS char: inputChar
        { /*** Reads the first (index 0) character of the string, puts it to lower and returns it. */
            string inputString;
            char inputChar;
            do
            {
                inputString = Console.ReadLine().ToLower();
                if (inputString == string.Empty)
                    Console.Write("An input is necessary: ");
            } while (inputString == string.Empty);

            inputChar = inputString[0];
            return inputChar;
        }

        static string ReadtheString() // RETURNS string inpuString
        { /*** Reads a string from the input and returns it. */
            string inputString;
            do
            {
                inputString = Console.ReadLine();
                if (inputString == string.Empty)
                    Console.Write("An input is necessary: ");
            } while (inputString == string.Empty);

            return inputString;
        }

        static int GetNumberFromaChar() // RETURNS int the Number
        { /*** Reads a char and converts it to a number between 0 to 9. */
            int theNumber;
            char c;
            do
            {
                c = ReadtheKey();
                theNumber = Convert.ToInt32(c) - 48;
                if (!(theNumber >= 0 && theNumber <= 9))
                    Console.Write("A proper input is necessary, a single digit between 0 to 9: ");
                } while (!(theNumber >= 0 && theNumber <= 9));

            return theNumber;
        }

        static void PrintWarriorsStatus(Warrior warrior)
        {
            Console.WriteLine("{0}, your status is:", warrior.Name);
            Console.WriteLine("Health: {0}, Exp: {1}, Armor: {2}, Weapon: {3}", warrior.Health, warrior.Experience, warrior.Armorx.Name, warrior.Weaponx.Name);
        }

        static bool DidHeroLevelUp(int currentExperience, int previousExperience) // RETURNS bool true/false
        { // check if warrior did or didn't level up (is current experience bigger than previous one? than hero did level up)
            if (currentExperience > previousExperience)
            {
                Console.WriteLine("Warrior leveled up!");
                return true;
            }

            return false;
        }

        static void PrintenemyWasKilled(List<Enemy> enemies)
        {
            Console.WriteLine();
            Console.WriteLine("The enemy was killed and you gained experience! remaining enemies: {0}", enemies.Count);
            Console.WriteLine();
        }

        /***
        * MAIN --------------------------------------------------------------------------------------------------------------------------------
        */
        static void Main(string[] args)
        {
            PrintHeadder();
            Armor someDefaultArmor = new Armor("some default armor", 0, 0); // used only for initialization
            Armor paperArmor = new Armor("paper armor", 2, 0);
            Armor vanguard = new Armor("Vanguard", 10, 2); // default armor
            Armor shivasGuard = new Armor("Shiva's", 20, 4);
            Armor[] armorUpgradeField = new Armor[2]; // upgrade armor field
            armorUpgradeField[0] = vanguard;
            armorUpgradeField[1] = shivasGuard;

            Weapon someDefaultWeapon = new Weapon("some default weapon", 0, 0); // used only for initialization
            Weapon kravMaga = new Weapon("krav maga", 20, 0); // default weapon
            Weapon kitchenKnife = new Weapon("kitchen knife", 30, 2);
            Weapon dagger = new Weapon("dagger", 40, 4);
            Weapon fnFAL = new Weapon("FN FAL", 101, 7);
            Weapon[] weaponUpgradeField = new Weapon[3]; // upgrade weapon field
            weaponUpgradeField[0] = kitchenKnife;
            weaponUpgradeField[1] = dagger;
            weaponUpgradeField[2] = fnFAL;

            bool warriorIsAliveFlag = true;
            Console.Write("Enter your name: ");
            string warriorsName = ReadtheString();
            Warrior warrior = new Warrior();
            warrior.Name = warriorsName;
            warrior.Armorx = someDefaultArmor;
            warrior.Weaponx = someDefaultWeapon;
            PrintWarriorsStatus(warrior);

            // load warrior with default values
            warrior.Weaponx = kravMaga;
            warrior.Armorx = paperArmor;
            PrintWarriorsStatus(warrior);

            // create enemies
            int maxEnemies = 15;
            int numberOfEnemies = maxEnemies;
            List<Enemy> enemies = new List<Enemy>();
            for (int i = 0; i < numberOfEnemies; i++)
                enemies.Add(new Enemy(kitchenKnife));

            while(true)
            {
                if (enemies.Count <= 0)
                    break;

                int previousWarriorsExperience = warrior.Experience; // store previous experience to check if the hero leveled up or not
                Console.WriteLine("Enemy's round! This enemy uses: {0}", enemies[0].Weaponx.Name);
                int damageByEnemy = warrior.Armorx.ProtectionFactor - enemies[0].Weaponx.Damage;
                if (damageByEnemy > 0)
                    damageByEnemy = 0;

                Console.WriteLine("total damage inflicted: {0}", Math.Abs(damageByEnemy));
                warrior.Health += damageByEnemy; // damage by enemy is always 0 or less
                

                if (warrior.Health <= 0)
                {
                    warriorIsAliveFlag = false;
                    break;
                }

                Console.WriteLine("Yours round!"); 
                enemies[0].Health -= warrior.Weaponx.Damage;
                if (enemies[0].Health <= 0)
                { 
                    enemies.Remove(enemies[0]);
                    warrior.Experience++;
                    PrintenemyWasKilled(enemies);
                }
                PrintWarriorsStatus(warrior);

                bool didHeroLevelUp = DidHeroLevelUp(warrior.Experience, previousWarriorsExperience);
                
                // choose better weapon
                if (didHeroLevelUp == true && enemies.Count > 0) // if hero leveled up, offer better weapons
                {
                    previousWarriorsExperience = warrior.Experience; // and make previous experience default again
                    bool whileLoopWasEnteredFlag = false;
                    Console.WriteLine("You can choose now a better armor.");
                    int j = 0;
                    int nextArmorExp = armorUpgradeField[j].RequiredExperience;
                    while (warrior.Experience >= nextArmorExp)
                    {
                        whileLoopWasEnteredFlag = true;
                        Console.WriteLine("Press {0} to choose: {1}", j, armorUpgradeField[j].Name);
                        j++;
                        if (j > 1)
                            break;
                        nextArmorExp = armorUpgradeField[j].RequiredExperience;
                    }
                    if (whileLoopWasEnteredFlag == true)
                    {
                        int input = GetNumberFromaChar();
                        j--;
                        if (input >= j)
                            input = j;
                        warrior.Armorx = armorUpgradeField[input];
                        Console.WriteLine("Armor changed to: {0}", warrior.Armorx.Name);
                    }

                    Console.WriteLine("You can choose now a better weapon.");
                     
                    // choose one ooooof available weapons
                    int i = 0;
                    int nextWeaponExp = weaponUpgradeField[i].RequiredExperience;
                    whileLoopWasEnteredFlag = false;
                    while (warrior.Experience >= nextWeaponExp)
                    {
                        whileLoopWasEnteredFlag = true;
                        Console.WriteLine("Press {0} to choose: {1}.", i, weaponUpgradeField[i].Name);
                        i++;
                        if (i > 2) // bigger than weaponUpgradefield
                            break;
                        nextWeaponExp = weaponUpgradeField[i].RequiredExperience;
                        
                    }
                    if (whileLoopWasEnteredFlag == true)
                    {
                        int input = GetNumberFromaChar();
                        i--; // guarantee that input will not be bigger than number of possible weapons to upgrade
                        if (input >= i)
                            input = i;
                        warrior.Weaponx = weaponUpgradeField[input];
                        Console.WriteLine("Weapon changed to: {0}.", warrior.Weaponx.Name);
                    }

                    Console.Write("To finish upgrading, hit return to continue...");
                    Console.ReadKey();
                }
            }

            if (warriorIsAliveFlag == false)
                Console.WriteLine("You died!");
            else
                Console.WriteLine("You won!");

            Console.Write("Hit return to continue...");
            Console.ReadKey();
        }
    }
}
