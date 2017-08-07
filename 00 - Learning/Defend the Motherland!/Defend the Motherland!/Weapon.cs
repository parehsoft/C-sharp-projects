using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defend_the_Motherland_
{   /***
    * WEAPON class - contains information about weapon:
    * 1) name - what weapon will be used? (knife, fork, sword, etc...)
    * 2) damage - what damage will the weapon deal
    * 3) requred experience - more soffisticated weapons will require more experience
    ***/
    class Weapon
    {
        /*** NAME ---------------------------------------------- */
        private string name = "fists";
        public string Name // --- property
        { // property to get and set the name
            get { return name; }
            set
            {
                if (value == String.Empty)
                    name = "name was not set, thus using: fists";
                else
                    name = value;
            }
        }

        /*** DAMAGE -------------------------------------------- */
        private int damage;
        public int Damage // --- property
        { // property to get and set the damage
            get { return damage;  }
            set { damage = value; }
        }

        /*** REQUIRED EXPERIENCE ------------------------------- */
        private int requiredExperience;
        public int RequiredExperience // --- property
        { // property to get and set the required experience
            get { return requiredExperience;  }
            set { requiredExperience = value; }
        }

        /* **** CONSTRUCTORs *-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*/
        public Weapon() { } // empty constructor
        public Weapon(string Name, int Damage, int RequiredExperience)
        { // normal constructor to fill the weapon's attributes
            this.Name = Name;
            this.Damage = Damage;
            this.RequiredExperience = RequiredExperience;
           // this.Index = Index;
        }

    }
}
