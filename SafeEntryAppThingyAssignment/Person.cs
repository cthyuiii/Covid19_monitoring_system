using System;
using System.Collections.Generic;
using System.Text;

namespace SafeEntryAppThingyAssignment
{
    abstract class Person
    {

        //Class Properties

        public string Name { get; set; }

        public List<SafeEntry> SafeEntryList = new List<SafeEntry>();

        public List<TravelEntry> TravelEntryList = new List<TravelEntry>();

        public List<Vaccine> VaccineList = new List<Vaccine>();

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
            foreach (TravelEntry te in TravelEntryList)
            {
                if (t.EntryDate == te.EntryDate)
                {
                    Console.WriteLine("Person Already Exists, unable to add to TravelEntry list");
                    found = true;
                }
            }
            if (found == false)
            {
                TravelEntryList.Add(t);
            }

        }

        //Method to add and check Safe entry record
        //If it exists will return error message saying record exists
        //Else it will add it into the List

        public void AddSafeEntry(SafeEntry s)
        {
            bool found = false;
            foreach (SafeEntry se in SafeEntryList)
            {
                if (s.CheckIn == se.CheckIn) // if person exists
                {
                    Console.WriteLine("Person Already Exists, unable to add to list");
                    found = true;
                }
            }
            if (found == false)
            {
                SafeEntryList.Add(s);
            }

        }

        public void AddVaccine(Vaccine v)
        {
            bool found = false;
            foreach (Vaccine va in VaccineList)
            {
                if (v.mrn == v.mrn) // if person exists
                {
                    Console.WriteLine("Person Already Exists, unable to add to list");
                    found = true;
                }
            }
            if (found == false)
            {
                VaccineList.Add(v);
            }

        }

        //Abstract Method for resident and visitor classes

        public abstract double CalculateSHNCharges();

        //To string method

        public override string ToString()
        {
            return "Name: " + Name;
        }
    }

   
}
