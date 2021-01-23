using System;
using System.Collections.Generic;
using System.Text;

namespace SafeEntryAppThingyAssignment
{
    abstract class Person
    {

        //Class Properties

        public string Name { get; set; }

        public List<SafeEntry> safeEntryList = new List<SafeEntry>();

        public List<TravelEntry> travelEntryList = new List<TravelEntry>();

        public Person()
        {

        }

        //Class Constructor

        public Person(string name)
        {
            Name = name;
        }

        //Method to add and check travel entry record
        //If it exists will return error message saying record exists
        //Else it will add it into the List

        public void AddTravelEntry(TravelEntry t)
        {
            bool found = false;
            foreach (TravelEntry te in travelEntryList)
            {
                if (t.EntryDate == te.EntryDate)
                {
                    Console.WriteLine("Person Already Exists, unable to add to list");
                    found = true;
                }
            }
            if (found == false)
            {
                travelEntryList.Add(t);
            }

        }

        //Method to add and check Safe entry record
        //If it exists will return error message saying record exists
        //Else it will add it into the List

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

        //Abstract Method for resident and visitor classes

        public abstract double CalculateSHNCharges();

        //To string method

        public override string ToString()
        {
            return "\tName: " + Name;
        }
    }

   
}
