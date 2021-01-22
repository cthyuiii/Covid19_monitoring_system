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

        public Resident(string name, string address, DateTime lastLeftCountry, TraceTogetherToken token):base(name)
        {
            Address = address;
            LastLeftCountry = lastLeftCountry;
            Token = token;
        }

        //incomplete

        /*public override double CalculateSHNCharges()
        {
            if(14 days)
            {
                return (200 + 20 + 1000)*1.07;
            }
            else if(7 days)
            {
                return (200 + 20) * 1.07;
            }
            else
            {
                return 200 * (1.07);
            }
        }*/

        public override string ToString()
        {
            return base.ToString() + "\tAddress: " + Address + "\tLast Left Country: " + LastLeftCountry + "\tToken: " + Token;
        }
    }
}
