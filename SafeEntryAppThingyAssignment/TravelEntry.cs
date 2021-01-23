using System;
using System.Collections.Generic;
using System.Text;

namespace SafeEntryAppThingyAssignment
{
    class TravelEntry
    {

        //Class Properties

        public string LastCountryOfEmbarkation { get; set; }

        public string EntryMode { get; set; }

        public DateTime EntryDate { get; set; }

        public DateTime ShnEndDate { get; set; }

        public SHNFacility ShnStay { get; set; }

        public bool IsPaid { get; set; }

        public TravelEntry()
        {

        }

        //Class Constructor

        public TravelEntry(string lastCountryOfEmbarkation, string entryMode, DateTime entryDate)
        {
            LastCountryOfEmbarkation = lastCountryOfEmbarkation;
            EntryMode = entryMode;
            EntryDate = entryDate;
        }

        //Assigns a person into a shn facility thereby reducing vacancy slots by 1

        public void AssignSHNFacility(SHNFacility shn)
        {
            ShnStay = shn;
            shn.FacilityVacancy -= 1;
        }

        //Method to calculate SHN Duration
        //As stated in the document
        // Persons coming from NZ or Vietname serve 0 days as they are two countries with 0 cases at the moment
        // As well as people from Macao serve 7 days
        //Else any other country person has to serve 14 days

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

        //To String method

        public override string ToString()
        {
            return base.ToString() + "\tLast Country Of Embarkation: " + LastCountryOfEmbarkation + "\tEntry Mode: " + EntryMode
                + "\tEntry Date: " + EntryDate + "\tSHN End Date: " + ShnEndDate + "\tSHN Location: " + ShnStay + "\tPaid?: " + IsPaid;
        }
    }
}
