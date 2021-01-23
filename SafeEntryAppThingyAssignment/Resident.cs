using System;
using System.Collections.Generic;
using System.Text;

namespace SafeEntryAppThingyAssignment
{
    //Inherit Properties from Abstract Person Class

    class Resident:Person
    {

        //Class Properties

        public string Address { get; set; }

        public DateTime LastLeftCountry { get; set; }

        public TraceTogetherToken Token { get; set; }

        //Class Constructor

        public Resident(string name, string address, DateTime lastLeftCountry):base(name)
        {
            Address = address;
            LastLeftCountry = lastLeftCountry;
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
                        return (200 + 20 + 1000) * 1.07;
                    }
                    else if (days == 7)
                    {
                        return (200 + 20) * 1.07;
                    }
                    else
                    {
                        return (200) * 1.07;
                    }
                }
            }
            return (0);
        }

        //To string method

        public override string ToString()
        {
            return base.ToString() + "\tAddress: " + Address + "\tLast Left Country: " + LastLeftCountry + "\tToken: " + Token;
        }
    }
}
