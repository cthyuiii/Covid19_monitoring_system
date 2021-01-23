using System;
using System.Collections.Generic;
using System.Text;

namespace SafeEntryAppThingyAssignment
{

    //Inherit Properties from Abstract Person Class

    class Visitor :Person
    {
        //Class Properties
        public string PassportNo { get; set; }

        public string Nationality { get; set; }

        //Class Constructor

        public Visitor(string name, string passportNo, string nationality):base(name)
        {
            PassportNo = passportNo;
            Nationality = nationality;
        }

        //Method overrides the abstract method in Person Class
        //Checks and calculates if a SHN Charge has been paid
        //Input received from user will be checked
        //If it is already paid it will display that the charges are paid
        //Else it will check how many days resident has been in facility 
        //and calculate and ammend charges to user before prompting for payment.

        public override double CalculateSHNCharges()
        {
            foreach (TravelEntry te in base.TravelEntryList)
            {
                if (te.IsPaid == true)
                {
                    Console.WriteLine("SHN Charges is already paid!");
                    break;
                }
                else
                {
                    double days = (te.ShnEndDate - te.EntryDate).TotalDays;
                    if (days == 14)
                    {
                        return (200 + 2000) * 1.07;
                    }
                    else 
                    {
                        return (200 + 80) * 1.07;
                    }
                    
                }
            }
            return (0);
        }

        //To string method
        
        public override string ToString()
        {
            return base.ToString() + "\tPassport No: " + PassportNo + "\tNationality: " + Nationality;
        }
    }
}
