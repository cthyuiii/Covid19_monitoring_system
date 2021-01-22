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

        //incomplete double
        // if loop required to check shnmode for calculation of swab test+ transporation
        // formulas is as follows for visitor:
        // no shn or 7-day shn at own accomodation : 200*1.07 + 80
        // 14 day @ shn facility : 200*1.07 + 2000 + (50 + (distance)*0.22)*1.25 (if entry 6am to 8:59am or 6pm  to 11:59pm)
        // 14 day @ shn facility : 200*1.07 + 2000 + (50 + (distance)*0.22)*1.50 (if entry 12am(midnight) to 5:59am)
        /*public override double CalculateSHNCharges()
        {
            if (14 days)
            {
                return (200 + 2000) * 1.07;
            }
            else 
            {
                return (200 + 80) * 1.07;
            }
        }*/

        public override string ToString()
        {
            return base.ToString() + "\tPassport No: " + PassportNo + "\tNationality: " + Nationality;
        }
    }
}
