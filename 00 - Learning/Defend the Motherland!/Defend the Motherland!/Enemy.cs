namespace Defend_the_Motherland_
{ /*** 
  * ENEMY calss contains description of the enemy :-) .
  * - int health - between 0 to 100
  * - int weapon - the weapon they will use
  */
    class Enemy
    {
        /*** HEALT -------------------------------------------------------------- */
        private int health = 100; // their health is 20
        public int Health // --- property
        { // property to get and set the health
            get { return health; }
            set
            {
                if (value < 0)
                    health = 0;
                else if (value > 100)
                    health = 100;
                else
                    health = value;
            }
        }

        /*** WEAPON ------------------------------------------------------------- */
        private Weapon weaponx;
        public Weapon Weaponx // --- property
        { // property to get and set weapon
            get { return weaponx;  }
            set { weaponx = value; }
        }

        /*** ------------------------------------------------------------------- */
        /*** CONSTRUCTORS ----------------------------------------------------------------------------------------- */
        /*** ------------------------------------------------------------------- */
        public Enemy() { } // empty constructor
        public Enemy(Weapon Weaponx) // constructor to set the weapon
        {
            this.Weaponx = Weaponx;
        }
    }
}