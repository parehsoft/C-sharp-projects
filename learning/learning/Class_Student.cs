using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace learning
{
    public class Student
    {
        private string firstname;
        public string Firstname { get { return firstname; } set { firstname = value; } } // property
        private string lastname;
        public string Lastname { get { return lastname; } set { lastname = value; } }
        private DateTime birthdate;
        public DateTime Birthdate { get { return birthdate; } set { birthdate = value; } }
        private string addressline1;
        public string Addressline1 { get { return addressline1; } set { addressline1 = value ; } }
        private string addressline2;
        public string Addressline2 { get { return addressline2; } set { addressline2 = value; } }
        private string city;
        public string City { get { return city; } set { city = value; } }
        private string state;
        public string State { get { return state; } set { state = value; } }
        private string postalcode;
        public string Postalcode { get { return postalcode; } set { postalcode = value; } }
        private string country;
        public string Country { get { return country; } set { country = value; } }

        public Student(){} // empty constructor
        public Student(string Firstname, string Lastname, DateTime Birthdate, string Addressline1, string Addressline2, string City, string State, string Postalcode, string Country)
        { // full constructor
            this.Firstname = Firstname;
            this.Lastname = Lastname;
            this.birthdate = Birthdate;
            this.addressline1 = Addressline1;
            this.addressline2 = Addressline2;
            this.city = City;
            this.state = State;
            this.postalcode = Postalcode;
            this.country = Country;
        }
    }
}
