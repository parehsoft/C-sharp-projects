using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace learning
{
    
    
    class Program
    {

        static Student student;

        static void PrintChessBoard() // -----------------------------------------------------------------------------
        {
            for (int i = 0; i < 8; i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        Console.Write("O");
                        Console.Write("X");
                    }
                }
                else
                {
                    for (int j = 0; j < 4; j++)
                    {
                        Console.Write("X");
                        Console.Write("O");
                    }
                }
                Console.WriteLine();
            }
        }

        static void FillAndCreateObjectOfStudent() // -----------------------------------------------------------------------------
        {
            Console.WriteLine("Enter student's first name:");
            string first_name = Console.ReadLine();
            
            Console.WriteLine("Enter student's last name:");
            string last_name = Console.ReadLine();

            Console.WriteLine("Enter student's date of birth:");
            DateTime date = new DateTime();
            int looprepeat = 0;
            do
            {
                try 
                { 
                    date = DateTime.Parse(Console.ReadLine()); 
                    looprepeat = 0; // in case it is ok, set looprepeat to (NO)
                }
                catch 
                {
                    Console.WriteLine("Wrong date format, please repeat.");
                    looprepeat = 1; // in case it is correct, set looprepeater to 1 (YES) 
                }
            } while (looprepeat == 1);
                     

            Console.WriteLine("Enter student's Addressline 1:");
            string addressline_1 = Console.ReadLine();

            string addressline_2 = String.Empty;
            Console.WriteLine("Is student's address line 2 empty? Y/N: ");
            string yes_no = Console.ReadLine();
            if (yes_no == "N" || yes_no == "n")
                addressline_2 = Console.ReadLine();

            Console.WriteLine("Enter student's city of permanent residence:");
            string city = Console.ReadLine();

            string state = String.Empty;
            Console.WriteLine("Is student's province empty? Y/N: ");
            yes_no = Console.ReadLine();
            if (yes_no == "N" || yes_no == "n")
                state = Console.ReadLine();

            Console.WriteLine("Enter student's postal code:");
            string postal_code = Console.ReadLine();

            Console.WriteLine("Enter student's country:");
            string country = Console.ReadLine();

            // And declare Student type object.
            student = new Student(first_name, last_name, date, addressline_1, addressline_2, city, state, postal_code, country);
        }

        static void PrintStudentInfo() // -----------------------------------------------------------------------------
        {
            Console.WriteLine();
            Console.WriteLine("--- STUDENT INFO ---");
            Console.WriteLine("Fisrtname is: {0}", student.Firstname);
            Console.WriteLine("Lastname is: {0}", student.Lastname);
            Console.WriteLine("Date of birth is: {0}", student.Birthdate);
            Console.WriteLine("1st addressline is: {0}", student.Addressline1);
            
            if (student.Addressline2 == String.Empty)
                Console.WriteLine("2nd addressline is empty.");
            else
                Console.WriteLine("2nd addressline is: {0}", student.Addressline2);
            
            Console.WriteLine("City is: {0}", student.City);

            if (student.State == String.Empty)
                Console.WriteLine("Province is empty.");
            else
                Console.WriteLine("Province is: {0}", student.State);
            
            Console.WriteLine("Postal code is: {0}", student.Postalcode);
            Console.WriteLine("Country is: {0}", student.Country);
            Console.WriteLine("--- END OF INFO ---");
        }

        enum Day { Monday, Tuesday, Whednesday, Thursday, Friday, Saturday, Sunday };
        /**
         * ----------------- MAIN -----------------
         */
        static int Main(string[] args)
        {
            PrintChessBoard();

            Day favouriteDay = (Day)4;

            Console.WriteLine("{0}", favouriteDay);


            FillAndCreateObjectOfStudent();
            PrintStudentInfo();



            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return 0;
        }
    }
}
