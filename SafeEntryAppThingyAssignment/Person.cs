using System;
using System.Collections.Generic;
using System.Text;

namespace SafeEntryAppThingyAssignment
{
    abstract class Person
    {
        public string Name { get; set; }

        List<SafeEntry> safeEntryList = new List<SafeEntry>();

        List<TravelEntry> travelEntryList = new List<TravelEntry>();

        public Person()
        {

        }

        public Person(string name)
        {
            Name = name;
        }

        public void AddTravelEntry(TravelEntry t)
        {
            bool found = false;
            foreach (TravelEntry te in travelEntryList)
            {
                if (t.LastCountryOfEmbarkation == te.LastCountryOfEmbarkation)
                {
                    Console.WriteLine("Peron Already Exists, unable to add to list");
                    found = true;
                }
            }
            if (found == false)
            {
                travelEntryList.Add(t);
            }

        }

        public void AddSafeEntry(SafeEntry s)
        {
            bool found = false;
            foreach (SafeEntry se in safeEntryList)
            {
                if (s.CheckIn == se.CheckIn) // if patient exists
                {
                    Console.WriteLine("Person Already Exists, unable to add to list");
                    found = true;
                }
            }
            if (found == false)
            {
                safeEntryList.Add(s);
            }

        }

        public abstract double CalculateSHNCharges();

        public override string ToString()
        {
            return "\tName: " + Name;
        }
    }

   
}
