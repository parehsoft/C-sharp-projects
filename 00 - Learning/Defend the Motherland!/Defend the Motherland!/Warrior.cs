using System;

namespace Defend_the_Motherland_
{   /***
    * WARRIOR class will contain information about warrior.
    * 1) Health - this can spread between 0 to 1000
    * 2) Experience - received experience for every killed attacking enemy
    * 4) Amor - used current armor
    * 5) Weapon - used weapon
    * 6) Aditional items - some aditional items which will be used later, maybe
    * 7) Name - your brother's wife's cousen's dog's name
    ***/
    class Warrior
    {
        /*** HEALT -------------------------------------------------------------- */
        private int health = 1000;
        public int Health // --- property
        { // property to get and set health
            get { return health; }
            set
            { // set to cut the health between 0 to 1000 in case if necessary
                if (value < 0)
                    health = 0;
                else if (value > 1000)
                    health = 1000;
                else
                    health = value;
            }
        }

        /*** EXPERIENCE --------------------------------------------------------- */
        private int experience = 0;
        public int Experience // --- property
        { // property to get and set the health
            get { return experience; }
            set
            { // experience will not be less than 0
                if (value < 0)
                    experience = 0;
                else
                    experience = value;
            }
        }

        /*** ARMOR -------------------------------------------------------------- */
        private Armor armorx;
        public Armor Armorx // --- property
        { // property to get and set armor to be used
            get { return armorx;  }
            set { armorx = value; }
        }

        /*** WEAPON ------------------------------------------------------------- */
        private Weapon weaponx;
        public Weapon Weaponx // --- property
        { // property to get and set weapon
            get { return weaponx;  }
            set { weaponx = value; }
        }

        /*** NAME --------------------------------------------------------------- */
        private string name = "Default Name";
        public string Name // --- property
        { // property to get and set the name
            get { return name; }
            set
            {
                if (value == String.Empty)
                    name = "Default Name";
                else
                    name = value;
            }
        }

        /*** POSSIBLE ADITIONAL ITEMS ------------------------------------------ */
        //private int[] inventory = new int[4];

        /*** *** ------------------------------------------------------------------- */
        /*** CONSTRUCTORS ----------------------------------------------------------------------------------------- */
        /*** *** ------------------------------------------------------------------- */
        public Warrior() { } // empty constructor
        /** No other constructor will be needed as there will only one instance of the warrior created in the entire program.
            Everything necessary will be set up through properties. */
    }
}
