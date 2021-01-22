using System;
using System.Collections.Generic;
using System.Text;

namespace SafeEntryAppThingyAssignment
{
    class Visitor:Person
    {
        public string PassportNo { get; set; }

        public string Nationality { get; set; }

        public Visitor(string name, string passportNo, string nationality):base(name)
        {
            PassportNo = passportNo;
            Nationality = nationality;
        }

        public override double CalculateSHNCharges()
        {
            foreach (TravelEntry te in base.travelEntryList)
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

        public override string ToString()
        {
            return base.ToString() + "\tPassport No: " + PassportNo + "\tNationality: " + Nationality;
        }
    }
}
