﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace SafeEntryAppThingyAssignment
{
    class Program
    {

        //============================================================
        // Student Number : S10194219, S10204877
        // Student Name : Macarius Dai, Tan Jian Xu
        // Module Group : T01
        //============================================================
        /*
        ▪ Validations (and feedback)
        ▪ The program should handle all invalid entries by the user
        e.g. invalid option, invalid year, invalid month, invalid day, etc.
        ▪ If user made a mistake in the entry, the program should inform the user via appropriate feedback
        ▪ The program is to display appropriate feedback messages (e.g., successful or not successful)
        */
        static void Main(string[] args)
        {
            List<BusinessLocation> businesslocationList = new List<BusinessLocation>();
           
            List<Person> personList = new List<Person>();

            List<SHNFacility> shnList = new List<SHNFacility>();

            InitDataPerson(personList);

            InitDataBusinessLocation(businesslocationList);

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://covidmonitoringapiprg2.azurewebsites.net");
                Task<HttpResponseMessage> responseTask = client.GetAsync("/facility");
                responseTask.Wait();

                HttpResponseMessage result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    Task<string> readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    string data = readTask.Result;

                    // deserialize JSON Text string to object
                    shnList = JsonConvert.DeserializeObject<List<SHNFacility>>(data);
                }
            };

            while (true)
            {
                string option = DisplayMenu();
                if (option == "1")
                {
                    DisplayVisitors(personList);
                }
                else if (option == "2")
                {
                    DisplayPerson(personList);
                }
                else if (option == "3")
                {
                    AssignReplaceToken(personList);
                }
                else if (option == "4")
                {
                    DisplayBusinessLocation(businesslocationList);
                }
                else if (option == "5")
                {
                    EditBusinessLocationCapacity(businesslocationList);
                }
                else if (option == "6")
                {
                    SafeEntryCheckin(businesslocationList, personList);
                }
                else if (option == "7")
                {
                    SafeEntryCheckout(personList);
                }
                else if (option == "8")
                {
                    DisplaySHNFacilities(shnList);
                }
                else if (option == "9")
                {
                    CreateVisitor(personList);
                }
                else if (option == "10")
                {
                    CreateTravelEntry(personList, shnList);
                }
                else if (option == "11")
                {
                    CheckSHNCharges(personList, shnList);
                }
                else if (option == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option!");
                    Console.WriteLine("Please Re-Enter only the option's Number");
                }
            }
        }

        //Method to Load Business Location data from csv file as well as adding data into it

        static void InitDataBusinessLocation(List<BusinessLocation> blList)
        {
            string[] csvLines = File.ReadAllLines("BusinessLocation.csv");
            for (int i = 1; i < csvLines.Length; i++)
            {
                string[] data = csvLines[i].Split(',');
                string businessName = data[0];
                string branchCode = data[1];
                int maximumCapcity = Convert.ToInt32(data[2]);
                blList.Add(new BusinessLocation(businessName, branchCode, maximumCapcity));
            }
            
        }

        //Method to Load Person data from csv file as well as adding data into it

        static void InitDataPerson(List<Person> pList)
        {
            string[] csvLines = File.ReadAllLines("Person.csv");
            for (int i = 1; i < csvLines.Length; i++)
            {
                string[] data = csvLines[i].Split(',');
                string type = data[0];
                string name = data[1];
                string address = data[2];
                string passportNo = data[4];
                string nationality = data[5];
                string tokenSerial = data[6];
                string tokenCollectionLocation = data[7];
                string travelEntryLastCountry = data[9];
                string travelEntryMode = data[10];
                string facilityName = data[14];
                string lastLeftCountry1 = data[3].ToString();
                string tokenExpiryDate1 = data[8].ToString();
                string travelEntryDate1 = data[11].ToString();
                string travelShnEndDate1 = data[12].ToString();
                string travelIsPaid1 = data[13].ToString();
                DateTime lastLeftCountry = new DateTime();
                DateTime tokenExpiryDate = new DateTime();
                DateTime travelEntryDate = new DateTime();
                DateTime travelShnEndDate = new DateTime();
                bool travelIsPaid = new bool();
                if (String.IsNullOrEmpty(lastLeftCountry1))
                {
                }
                else
                {
                    lastLeftCountry = Convert.ToDateTime(lastLeftCountry1);
                }
                if (String.IsNullOrEmpty(tokenExpiryDate1))
                {
                }
                else
                {
                    tokenExpiryDate = Convert.ToDateTime(tokenExpiryDate1);
                }
                if (String.IsNullOrEmpty(travelEntryDate1))
                {
                }
                else
                {
                    travelEntryDate = Convert.ToDateTime(travelEntryDate1);
                }
                if (String.IsNullOrEmpty(travelShnEndDate1))
                {
                }
                else
                {
                    travelShnEndDate = Convert.ToDateTime(travelShnEndDate1);
                }
                if (String.IsNullOrEmpty(travelIsPaid1))
                {
                }
                else
                {
                    travelIsPaid = Convert.ToBoolean(travelIsPaid1);
                }
                if (type == "visitor")
                {
                    Visitor v = new Visitor(name, passportNo, nationality);
                    pList.Add(v);
                    v.AddTravelEntry(new TravelEntry(travelEntryLastCountry, travelEntryMode, travelEntryDate));
                }
                else
                {
                    Resident r = new Resident(name, address, lastLeftCountry);
                    pList.Add(r);
                    if (tokenSerial == "")
                    {

                    }
                    else
                    {
                        r.Token = new TraceTogetherToken(tokenSerial, tokenCollectionLocation, tokenExpiryDate);
                    }
                    r.AddTravelEntry(new TravelEntry(travelEntryLastCountry, travelEntryMode, travelEntryDate));
                }
            }
        }

        //3) Method to List all Visitors
        static void DisplayVisitors(List<Person> pList)
        {
            foreach (Person p in pList) 
            {
                if (p is Visitor)
                {
                    Visitor v = (Visitor)p;
                    Console.WriteLine(v.ToString());
                }
            }
        }

        /*4) Method to List Person Details
            1. prompt user for name
            2. search for person
            3. list person details including TravelEntry and SafeEntry details
            i. if resident, display TraceTogetherToken details
        */

        static void DisplayPerson(List<Person> pList)
        {
            Console.Write("Enter Name (Be mindful of capitalization. Enter 0 to exit option): ");
            string name = Console.ReadLine();
            Console.WriteLine(SearchPerson(pList, name));
        }

        static Person SearchPerson(List<Person> pList, string name)
        {
            while (true)
            {
                foreach (Person p in pList)
                {
                    if (p.Name == name)
                    {
                        return p;
                    }
                    else if (name == "0")
                    {
                        return null;
                    }
                }
                Console.WriteLine("Invalid Name!");
                Console.WriteLine("Please Re-Enter");
            }
        }

        /*5) Method to Assign/Replace TraceTogether Token
            1. prompt user for name
            2. search for resident name
            3. create and assign a TraceTogetherToken object if resident has no existing token
            4. replace token if it meets the criteria stipulated in the background brief
        */

        static Person AssignReplaceToken(List<Person> pList)
        {
            while (true)
            {
                Console.Write("Enter name (Be mindful of capitalization. Enter 0 to exit option): ");
                string name = Console.ReadLine();
                foreach (Person p in pList)
                {
                    if (p is Resident)
                    {
                        Resident r = (Resident)p;
                        if (r.Name == name)
                        {
                            if (r.Token == null)
                            {
                                r.Token = new TraceTogetherToken("T1", "C", DateTime.Now.AddMonths(6));
                                Console.WriteLine("TraceTogether Token assigned!");
                            }
                            else
                            {
                                bool replace = r.Token.IsEligibleForReplacement();
                                if (replace == true)
                                {
                                    r.Token.ReplaceToken("T2", "A");
                                    Console.WriteLine("TraceTogether Token replaced!");
                                }
                                else
                                {
                                    Console.WriteLine("TraceTogether not eligible for replacement!");
                                    Console.WriteLine("Please Re-apply for it on gov.sg");
                                }
                            }
                            break;
                        }
                    }
                    else if (p is Visitor)
                    {
                        Console.WriteLine("Person is a Visitor, unable to assign token!");
                        break;
                    }
                    else if (name == "0")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Name!");
                        Console.WriteLine("Please Re-Enter");
                    }
                }
            }
        }

        //6) Method to List all Business Locations
        static void DisplayBusinessLocation(List<BusinessLocation> blList)
        {
            foreach (BusinessLocation bl in blList)
            {
                Console.WriteLine(bl.ToString());
            }
        }

        /*7) Method to Edit Business Location Capacity
            1. prompt user to enter details
            2. search for business location
            3. prompt user to edit maximum capacity
        */

        static BusinessLocation EditBusinessLocationCapacity(List<BusinessLocation> blList)
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter Business Location (Be mindful of capitalization. Enter 0 to exit option): ");
                    string location = Console.ReadLine();
                    foreach (BusinessLocation bl in blList)
                    {
                        if (bl.BusinessName == location)
                        {
                            while (true)
                            {
                                Console.Write("Enter new capacity (Type 0 to Exit): ");
                                int capacity = Convert.ToInt32(Console.ReadLine());
                                if (capacity == bl.MaximumCapacity)
                                {
                                    Console.WriteLine("Invalid Option: Input is same as current Capacity. Nothing to Change!");
                                    Console.WriteLine("Please Re-Enter");
                                }
                                else if (capacity == 0)
                                {
                                    break;
                                }
                                bl.MaximumCapacity = capacity;
                                Console.WriteLine(bl);
                                break;
                            }
                            break;
                        }
                        else if (location == "0")
                        {
                            break;
                        }
                    }
                    Console.WriteLine("Invalid Location/Location Doesn't Exist!");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Invalid Input!!");
                }
            }
        }

        /*8) Method for SafeEntry Check-in
            1. prompt user for name
            2. search for person
            3. list all business locations
            4. prompt user to select for business location to check-in
            5. create SafeEntry object if the location is not full, and increase visitorsNow count
            6. add SafeEntry object to person
        */

        static Person SafeEntryCheckin(List<BusinessLocation> blList, List<Person> pList)
        {
            while (true)
            {

                Console.Write("Enter Name (Be mindful of capitalization. Enter 0 to exit option): ");
                string name = Console.ReadLine();
                if (name == "0")
                {
                    return null;
                }
                else
                {
                    Person p = SearchPerson(pList, name);
                    Console.WriteLine("Which Location are you checking-in to?");
                    DisplayBusinessLocation(blList);
                    Console.Write("Enter Business Location (Enter 0 to Exit): ");
                    string locationname = Console.ReadLine();
                    if (locationname == "0")
                    {
                        return null;
                    }
                    foreach (BusinessLocation bl in blList)
                    {
                        if (bl.BusinessName == locationname)
                        {
                            bool full = bl.IsFull();
                            if (full == false)
                            {
                                p.AddSafeEntry(new SafeEntry(DateTime.Now, bl));
                                bl.VisitorsNow += 1;
                                Console.WriteLine("Successfully Checked-in at {0}!", locationname);
                                return p;
                            }
                        }
                        Console.WriteLine("Invalid Name/Location!");
                        Console.WriteLine("Please Re-Enter");
                    }
                }
                return null;
            }
        }

        /*9) Method for SafeEntry Check-out
            1. prompt user for name
            2. search for person
            3. list SafeEntry records for that person that have not been checked-out
            4. prompt user to select record to check-out
            5. call PerformCheckOut() to check-out, and reduce visitorsNow by 1
        */

        static SafeEntry SafeEntryCheckout(List<Person> pList)
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter Name (Be mindful of capitalization. Enter 0 to exit option): ");
                    string name = Console.ReadLine();
                    if (name == "0")
                    {
                        return null;
                    }
                    else
                    {
                        Person p = SearchPerson(pList, name);
                        for (int i = 0; i < p.SafeEntryList.Count; i++)
                        {
                            if (p.SafeEntryList.Count == 0)
                            {
                                Console.WriteLine("There is no SafeEntry Record!");
                                return null;
                            }
                            else
                            {
                                Console.WriteLine(p.SafeEntryList[i]);
                            }
                        }
                        Console.Write("Enter Record in the format dd/mm/yyyy: ");
                        DateTime record = Convert.ToDateTime(Console.ReadLine());
                        foreach (SafeEntry s in p.SafeEntryList)
                        {
                            Console.WriteLine("Successfully Checked-out!");
                            s.PerformCheckOut();
                            return s;
                        }
                        return null;
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Invalid Record/Input!");
                } 
                
            }
        }

        //10) Method to List all SHN Facilities
        static void DisplaySHNFacilities(List<SHNFacility> shnList) 
        {            
            foreach (SHNFacility shn in shnList)
            {
                Console.WriteLine(shn.ToString());
            }
        }

        /*11) Method to Create Visitor Object
            1. prompt user for details
            2. create Visitor object
        */

        static Visitor CreateVisitor(List<Person> pList)
        {
            while (true)
            {
                Console.Write("Enter Name (Be mindful of capitalization. Enter 0 to exit option): ");
                string name = Console.ReadLine();
                if(name =="0")
                {
                    return null;
                }
                Console.Write("Enter Passport No (Be mindful of capitalization. Enter 0 to exit option): ");
                string passport = Console.ReadLine();
                if(passport == "0")
                {
                    return null;
                }
                Console.Write("Enter Nationality (Be mindful of capitalization. Enter 0 to exit option): ");
                string nationality = Console.ReadLine();
                if (passport == "0")
                {
                    return null;
                }
                foreach (Visitor v in pList)
                {
                    if (passport == v.PassportNo)
                    {
                        Console.WriteLine("Person Already Exists!");
                        Console.WriteLine("Please Re-Enter");
                    }
                }
                Console.WriteLine("Succesfully Created!");
                pList.Add(new Visitor(name, passport, nationality));
                return null;
            }
        }

        /*12) Method to Create TravelEntry Record Object
            1. prompt user for name
            2. search for person
            3. prompt user for details (last country of embarkation, entry mode)
            4. create TravelEntry object
            5. call CalculateSHNDuration() to calculate SHNEndDate based on criteria given in the background brief
            6. list SHN facilities if necessary, for user to select
            7. assign chosen SHN facility if necessary, and reduce the vacancy count
            8. call AddTravelEntry() in Person to assign the TravelEntry object
        */

        static Person CreateTravelEntry(List<Person> pList, List<SHNFacility> shnList)
        {
            while (true)
            {
                Console.Write("Enter Name ((Be mindful of capitalization. Enter 0 to exit option): ");
                string name = Console.ReadLine();
                if(name == "0")
                {
                    return null;
                }    
                Person p = SearchPerson(pList, name);
                Console.Write("Enter your last country of embarkation (Be mindful of capitalization. Enter 0 to exit option): ");
                string last_country = Console.ReadLine();
                if (last_country == "0")
                {
                    return null;
                }
                Console.Write("Enter your entry mode (Be mindful of capitalization. Enter 0 to exit option): ");
                string entry_mode = Console.ReadLine();
                if (entry_mode == "0")
                {
                    return null;
                }
                p.AddTravelEntry(new TravelEntry(last_country, entry_mode, DateTime.Now));
                foreach (TravelEntry t in p.TravelEntryList)
                {
                    t.CalculateSHNDuration();
                    if ((t.ShnEndDate - t.EntryDate).TotalDays == 14)
                    {
                        DisplaySHNFacilities(shnList);
                        Console.Write("Choose SHN Facility (Be mindful of capitalization. Enter 0 to exit option): ");
                        string f_name = Console.ReadLine();
                        if (f_name == "0")
                        {
                            return null;
                        }
                        foreach (SHNFacility shn in shnList)
                        {
                            while (true)
                            {
                                if (shn.FacilityName == f_name)
                                {
                                    bool available = shn.IsAvailable();
                                    if (available)
                                    {
                                        t.AssignSHNFacility(shn);
                                        break;

                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Name!");
                                        Console.WriteLine("Please Re-Enter");
                                    }
                                }
                                else
                                    Console.WriteLine("Facility Not Available!");
                            }
                        }
                        p.AddTravelEntry(t);
                        break;
                    }
                }
                return null;
            }
        }



        /*13) Method to Calculate SHN Charges
            1. prompt user for name
            2. search for person
            3. retrieve TravelEntry with SHN ended and is unpaid
            4. call CalculateSHNCharges() to calculate the charges based on the criteria provided in the background brief
                i. Note: To add 7% GST
            5. prompt to make payment
            6. change the isPaid Boolean value
        */

        static void CheckSHNCharges(List<Person> pList, List<SHNFacility> shnList)
        {
            while (true)
            {
                Console.Write("Enter Name (Be mindful of capitalization. Enter 0 to exit option): ");
                string name = Console.ReadLine();
                if (name == "0")
                {
                    break;
                }
                Person p = SearchPerson(pList, name);
                if (p is Visitor)
                {
                    Visitor v = (Visitor)p;
                    foreach (TravelEntry te in v.TravelEntryList)
                    {
                        bool paid = te.IsPaid;
                        if (paid)
                        {
                            Console.WriteLine("Charges are Already Paid!");
                            break;
                        }
                        else
                        {
                            if (te.ShnEndDate < DateTime.Now)
                            {
                                Console.WriteLine(v.TravelEntryList[0]);
                                if ((te.ShnEndDate - te.EntryDate).TotalDays == 14)
                                {
                                    foreach (SHNFacility shn in shnList)
                                    {
                                        double charge = v.CalculateSHNCharges() + shn.CalculateTravelCost(te.EntryMode, te.EntryDate);
                                        Console.WriteLine("Please make Payment of {0}", charge);
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine(v.TravelEntryList[0]);
                                double charge = v.CalculateSHNCharges();
                                Console.WriteLine("Please make Payment of {0}", charge);
                            }
                        }
                        te.IsPaid = true;
                    }
                }
                else
                {
                    Resident r = (Resident)p;
                    foreach (TravelEntry te in r.TravelEntryList)
                    {
                        bool paid = te.IsPaid;
                        if (paid)
                        {
                            Console.WriteLine("Charges are Already Paid!");
                        }
                        else
                        {
                            if (te.ShnEndDate < DateTime.Now)
                            {
                                Console.WriteLine(r.TravelEntryList[0]);
                                if ((te.ShnEndDate - te.EntryDate).TotalDays == 14)
                                {
                                    foreach (SHNFacility shn in shnList)
                                    {
                                        double charge = r.CalculateSHNCharges() + shn.CalculateTravelCost(te.EntryMode, te.EntryDate);
                                        Console.WriteLine("Please make Payment of {0}", charge);
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine(r.TravelEntryList[0]);
                                double charge = r.CalculateSHNCharges();
                                Console.WriteLine("Please make Payment of {0}", charge);
                            }
                        }
                        te.IsPaid = true;
                    }
                }
            }
        }
        
        // Method to create and display menu
        
        static string DisplayMenu()
        {
            Console.WriteLine("---------------- MENU ----------------" +
                    "\n[1] List all visitors" +
                    "\n[2] List Person Details" +
                    "\n[3] Assign/Replace TraceTogether Token" +
                    "\n[4] List all Business Locations" +
                    "\n[5] Edit Business Location Capacity" +
                    "\n[6] SafeEntry Check-in" +
                    "\n[7] SafeEntry Check-out" +
                    "\n[8] List all SHN Facilities" +
                    "\n[9] Create Visitor" +
                    "\n[10] Create TravelEntry Record" +
                    "\n[11] Calculate SHN Charges" +
                    "\n[0] Exit" +
                    "\n-------------------------------------");
            Console.Write("Enter your option: ");
            string option = Console.ReadLine();
            return option;
        }
    }
}
/*
3.ADVANCED FEATURES
      You are required to do all the advanced features below.
        3.1 Contact Tracing Reporting
        • Given a date/time and business name, generate a list of persons that are checked-in at that location and period.
        • Export a CSV with details of their visit (e.g., check-in time, check-out time)
        3.2 SHN Status Reporting
        • Given a date, generate a csv report of all travellers serving their SHN, their SHN end date, and where they are serving their SHN.
        3.3 Other Possible Features
        • You may gain up to 5 bonus marks if you propose and successfully implement an additionalfeature. Check with your tutor with your idea before implementing.*/
