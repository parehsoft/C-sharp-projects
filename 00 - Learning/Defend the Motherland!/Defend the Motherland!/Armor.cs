using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defend_the_Motherland_
{   /***
    * ARMOR class - contains information about armor:
    * 1) name - what armor will be used? (forced steel, paper, chaimail, shivas guard)
    * 2) protection factor - what damage will the armor absorb
    * 3) requred experience - more sofisticated armors will require more experience
    ***/
    class Armor
    {
        /*** NAME ----------------------------------------------- */
        private string name = "no armor";
        public string Name // --- property
        { // property to get and set the name
            get { return name; }
            set
            {
                if (value == String.Empty)
                    name = "name was not set, thus using: no armor";
                else
                    name = value;
            }
        }

        /*** PROTECTION FACTOR  ---------------------------------- */
        private int protectionFactor;
        public int ProtectionFactor // --- property
        { // property to get and set the protectionFactor
            get { return protectionFactor;  }
            set { protectionFactor = value; }
        }

        /*** REQUIRED EXPERIENCE --------------------------------- */
        private int requiredExperience;
        public int RequiredExperience // --- property
        { // property to get and set the required experience
            get { return requiredExperience; }
            set { requiredExperience = value; }
        }

        /* **** CONSTRUCTORs *-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*/
        public Armor() { } // empty constructor
        public Armor(string Name, int ProtectionFactor, int RequiredExperience)
        { // normal constructor to fill the weapon's attributes
            this.Name = Name;
            this.ProtectionFactor = ProtectionFactor;
            this.RequiredExperience = RequiredExperience;
           // this.Index = Index;
        }
    }
}
