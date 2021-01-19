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

        public Resident(string address, DateTime lastLeftCountry, TraceTogetherToken token)
        {
            Address = address;
            LastLeftCountry = lastLeftCountry;
            Token = token;
        }

        //incomplete double

        public void CalculateSHNCharges()
        {
            
        }

        public override string ToString()
        {
            return base.ToString() + "Address: " + Address + "Last Left Country: " + LastLeftCountry + "Token: " + Token;
        }
    }
}
