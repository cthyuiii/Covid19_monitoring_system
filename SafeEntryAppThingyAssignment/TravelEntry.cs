﻿using System;
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

        //incomplete
        // visitors from new zealand and vietnam serve 0 day shn but require swab test
        // visitors from macao serve 7 day at home and requrie swab test
        // visitors from every other country serve 14 days at sdf and require swab test

        public void AssignSHNFacility(SHNFacility shn)
        {

        }

        //incomplete 

        public void CalculateSHNDuration()
        {

        }

        public override string ToString()
        {
            return base.ToString() + "Last Country Of Embarkation: " + LastCountryOfEmbarkation + "Entry Mode: " + EntryMode
                + "Entry Date: " + EntryDate + "SHN End Date: " + ShnEndDate + "SHN Location: " + ShnStay + "Paid?: " + IsPaid;
        }
    }
}
