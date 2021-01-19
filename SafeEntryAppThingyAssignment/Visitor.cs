using System;
using System.Collections.Generic;
using System.Text;

namespace SafeEntryAppThingyAssignment
{
    class Visitor:Person
    {
        public string PassportNo { get; set; }

        public string Nationality { get; set; }

        public Visitor(string passportNo, string nationality)
        {
            PassportNo = passportNo;
            Nationality = nationality;
        }

        //incomplete double
        public void CalculateSHNCharges()
        {

        }

        public override string ToString()
        {
            return base.ToString() + "Passport No: " + PassportNo + " Nationality: " + Nationality;
        }
    }
}
