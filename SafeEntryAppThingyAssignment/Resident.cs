using System;
using System.Collections.Generic;
using System.Text;

namespace SafeEntryAppThingyAssignment
{
    class Resident:Person
    {
        public string Address { get; set; }

        public DateTime LastLeftCountry { get; set; }

        public TraceTogetherToken Token { get; set; }

        public Resident(string name, string address, DateTime lastLeftCountry):base(name)
        {
            Address = address;
            LastLeftCountry = lastLeftCountry;
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

        public override string ToString()
        {
            return base.ToString() + "\tAddress: " + Address + "\tLast Left Country: " + LastLeftCountry + "\tToken: " + Token;
        }
    }
}
