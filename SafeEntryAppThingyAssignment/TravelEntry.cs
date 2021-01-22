using System;
using System.Collections.Generic;
using System.Text;

namespace SafeEntryAppThingyAssignment
{
    class TravelEntry
    {
        public string LastCountryOfEmbarkation { get; set; }

        public string EntryMode { get; set; }

        public DateTime EntryDate { get; set; }

        public DateTime ShnEndDate { get; set; }

        public SHNFacility ShnStay { get; set; }

        public bool IsPaid { get; set; }

        public TravelEntry()
        {

        }

        public TravelEntry(string lastCountryOfEmbarkation, string entryMode, DateTime entryDate, DateTime shnEndDate, 
            SHNFacility shnStay, bool isPaid)
        {
            LastCountryOfEmbarkation = lastCountryOfEmbarkation;
            EntryMode = entryMode;
            EntryDate = entryDate;
            ShnEndDate = shnEndDate;
            ShnStay = shnStay;
            IsPaid = isPaid;
        }

        public void AssignSHNFacility(SHNFacility shn)
        {
            ShnStay = shn;
            shn.FacilityVacancy -= 1;
        }

        //incomplete 

        public void CalculateSHNDuration()
        {
            if (LastCountryOfEmbarkation == "New Zealand" || LastCountryOfEmbarkation == "Vietnam")
            {
                ShnEndDate = EntryDate.AddDays(0);
            }
            else if (LastCountryOfEmbarkation == "Macao SAR")
            {
                ShnEndDate = EntryDate.AddDays(7);
            }
            else
            {
                ShnEndDate = EntryDate.AddDays(14);
            }
        }

        public override string ToString()
        {
            return base.ToString() + "\tLast Country Of Embarkation: " + LastCountryOfEmbarkation + "\tEntry Mode: " + EntryMode
                + "\tEntry Date: " + EntryDate + "\tSHN End Date: " + ShnEndDate + "\tSHN Location: " + ShnStay + "\tPaid?: " + IsPaid;
        }
    }
}
