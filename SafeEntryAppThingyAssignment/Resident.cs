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
        // if loop required to check shnmode for calculation of swab test+ transporation
        // formulas is as follows for resident:
        //if none just calculate swab test: 200*(1.07)
        //if 7 days : 200*(1.07) + 20
        // if 14 days: 200*(1.07) + 20 + 1000

        public void CalculateSHNCharges()
        {
            
        }

        public override string ToString()
        {
            return base.ToString() + "Address: " + Address + "Last Left Country: " + LastLeftCountry + "Token: " + Token;
        }
    }
}
