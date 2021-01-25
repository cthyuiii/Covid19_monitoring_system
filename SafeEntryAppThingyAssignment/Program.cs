using System;
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
        3. ADVANCED FEATURES
        You are required to do all the advanced features below.
        3.1 Contact Tracing Reporting
        • Given a date/time and business name, generate a list of persons that are checked-in at that location and period.
        • Export a CSV with details of their visit (e.g., check-in time, check-out time)
        3.2 SHN Status Reporting
        • Given a date, generate a csv report of all travellers serving their SHN, their SHN end date, and where they are serving their SHN.
        3.3 Other Possible Features
        • You may gain up to 5 bonus marks if you propose and successfully implement an additionalfeature. Check with your tutor with your idea before implementing.*/
        static void Main(string[] args)
        {
            List<BusinessLocation> businesslocationList = new List<BusinessLocation>();
           
            List<Person> personList = new List<Person>();

            List<SHNFacility> shnList = new List<SHNFacility>();

            InitDataPerson(personList);

            InitDataBusinessLocation(businesslocationList);

            InitSHNapiJson(shnList);

            while (true)
            {
                int option = DisplayMenu();
                if (option == 1)
                {
                    DisplayVisitors(personList);
                }
                else if (option == 2)
                {
                    DisplayPerson(personList);
                }
                else if (option == 3)
                {
                    AssignReplaceToken(personList);
                }
                else if (option == 4)
                {
                    DisplayBusinessLocation(businesslocationList);
                }
                else if (option == 5)
                {
                    EditBusinessLocationCapacity(businesslocationList);
                }
                else if (option == 6)
                {
                    SafeEntryCheckin(businesslocationList, personList);
                }
                else if (option == 7)
                {
                    SafeEntryCheckout(personList);
                }
                else if (option == 8)
                {
                    DisplaySHNFacilities(shnList);
                }
                else if (option == 9)
                {
                    CreateVisitor(personList);
                }
                else if (option == 10)
                {
                    CreateTravelEntry(personList, shnList);
                }
                else if (option == 11)
                {
                    CheckSHNCharges(personList, shnList);
                }
                else if (option == 0)
                {
                    break;
                }
                else
                    Console.WriteLine("Invalid option!");
            }
        }

        //2) Load SHN Facility Data
        //1.call API and populate a list
        //Method to Load SHN data from json api as well as adding data into it

        static void InitSHNapiJson(List<SHNFacility> sList)
        {
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
                    sList = JsonConvert.DeserializeObject<List<SHNFacility>>(data);
                    

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
                    r.Token = new TraceTogetherToken(tokenSerial, tokenCollectionLocation, tokenExpiryDate);
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
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            Console.WriteLine(SearchPerson(pList, name));
        }

        static Person SearchPerson(List<Person> pList, string name)
        {
            foreach (Person p in pList)
            {
                if (p.Name == name)
                {
                    return p;
                }
                else
                    Console.WriteLine("Invalid Name!");
            }
            return null;
        }

        /*5) Method to Assign/Replace TraceTogether Token
            1. prompt user for name
            2. search for resident name
            3. create and assign a TraceTogetherToken object if resident has no existing token
            4. replace token if it meets the criteria stipulated in the background brief
        */

        static void AssignReplaceToken(List<Person> pList)
        {
            Console.Write("Enter name: ");
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
                            r.Token = new TraceTogetherToken("T1", "C", new DateTime(2022, 1, 1));
                        }
                        bool replace = r.Token.IsEligibleForReplacement();
                        if (replace == true)
                        {
                            r.Token.ReplaceToken("T2", "A");
                        }
                    }
                    else
                        Console.WriteLine("Invalid Name!");
                }
            }
        }

        static Resident SearchResident(List<Person> pList, string name)
        {
            foreach (Person p in pList)
            {
                if (p is Resident)
                {
                    Resident r = (Resident)p;
                    if (r.Name == name)
                    {
                        return r;
                    }
                    else
                        Console.WriteLine("Invalid Name!");
                }
            }
            return null;
        }

        //6) Method to List all Business Locations
        static void DisplayBusinessLocation(List<BusinessLocation> blList)
        {
            Console.WriteLine("{0,10}  {1,10}  {2,10}",
                "Business Name", "Branch Code", "Maximum Capacity");
            foreach (BusinessLocation bl in blList)
            {
                Console.WriteLine("{0, 1} {1, -10} {2, -15}",
                        bl.BusinessName, bl.BranchCode, bl.MaximumCapacity);
            }
        }

        /*7) Method to Edit Business Location Capacity
            1. prompt user to enter details
            2. search for business location
            3. prompt user to edit maximum capacity
        */

        static void EditBusinessLocationCapacity(List<BusinessLocation> blList)
        {
            Console.Write("Enter Business Location: ");
            string location = Console.ReadLine();
            foreach (BusinessLocation bl in blList)
            {
                if (bl.BusinessName == location)
                {
                    Console.Write("Enter new capacity: ");
                    int capacity = Convert.ToInt32(Console.ReadLine());
                    bl.MaximumCapacity = capacity;
                }
                else
                    Console.WriteLine("Invalid Name!");
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

        static void SafeEntryCheckin(List<BusinessLocation> blList, List<Person> pList)
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            Person p = SearchPerson(pList, name);
            DisplayBusinessLocation(blList);
            Console.Write("Enter Business Location: ");
            foreach (BusinessLocation bl in blList)
            {
                if (bl.BusinessName == name)
                {
                    bool full = bl.IsFull();
                    if (full == false)
                    {
                        p.AddSafeEntry(new SafeEntry(DateTime.Now, bl));
                        bl.VisitorsNow += 1;
                    }
                }
                else
                    Console.WriteLine("Invalid Name!");
            }
        }

        /*9) Method for SafeEntry Check-out
            1. prompt user for name
            2. search for person
            3. list SafeEntry records for that person that have not been checked-out
            4. prompt user to select record to check-out
            5. call PerformCheckOut() to check-out, and reduce visitorsNow by 1
        */

        static void SafeEntryCheckout(List<Person> pList)
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            Person p = SearchPerson(pList, name);
            for (int i=0; i<p.SafeEntryList.Count; i++)
            {
                Console.WriteLine(p.SafeEntryList[i]);
            }
            Console.Write("Enter Record: ");
            DateTime record = Convert.ToDateTime(Console.ReadLine());
            foreach (SafeEntry s in p.SafeEntryList)
            {
                if (record == s.CheckIn)
                {
                    s.PerformCheckOut();
                }
            }
        }

        //10) Method to List all SHN Facilities
        static void DisplaySHNFacilities(List<SHNFacility> shnList) 
        {
            Console.WriteLine("{0,10}  {1,10}  {2,10}  {3 10}  {4 10}",
                "Name", "Capacity", "Distance from Air Checkpoint",
                "Distance from Sea Checkpoint", "Distance from Land Checkpoint");
            foreach (SHNFacility shn in shnList)
            {
                Console.WriteLine("{0, 1}  {1, -10}  {2, -15}  {3, 10}  {4, 10}",
                        shn.FacilityName, shn.FacilityCapacity, shn.DistFromAirCheckpoint,
                        shn.DistFromSeaCheckpoint, shn.DistFromLandCheckpoint);
            }
        }

        /*11) Method to Create Visitor Object
            1. prompt user for details
            2. create Visitor object
        */

        static void CreateVisitor(List<Person> pList)
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Passport No: ");
            string passport = Console.ReadLine();
            Console.Write("Enter Nationality: ");
            string nationality = Console.ReadLine();
            pList.Add(new Visitor(name, passport, nationality));
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

        static void CreateTravelEntry(List<Person> pList, List<SHNFacility> shnList)
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            Person p = SearchPerson(pList, name);
            Console.Write("Enter your last country of embarkation: ");
            string last_country = Console.ReadLine();
            Console.Write("Enter your entry mode: ");
            string entry_mode = Console.ReadLine();
            p.AddTravelEntry(new TravelEntry(last_country, entry_mode, DateTime.Now));
            foreach (TravelEntry t in p.TravelEntryList)
            {
                t.CalculateSHNDuration();
                if ((t.ShnEndDate - t.EntryDate).TotalDays == 14)
                {
                    DisplaySHNFacilities(shnList);
                    Console.Write("Choose SHN Facility: ");
                    string f_name = Console.ReadLine();
                    foreach (SHNFacility shn in shnList)
                    {
                        bool available = shn.IsAvailable();
                        if (available)
                        {
                            if (shn.FacilityName == f_name)
                            {
                                t.AssignSHNFacility(shn);
                            }
                            else
                                Console.WriteLine("Invalid Name!");
                        }
                        else
                            Console.WriteLine("Facility Not Available!");
                    }
                    p.AddTravelEntry(t);
                }
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
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            Person p = SearchPerson(pList, name);
            if (p is Visitor)
            {
                Visitor v = (Visitor)p;
                foreach (TravelEntry te in v.TravelEntryList)
                {
                    bool paid = te.IsPaid;
                    if (paid)
                    {
                        Console.WriteLine("Paid!");
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
                                    Console.WriteLine("Make Payment of {0}", charge);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine(v.TravelEntryList[0]);
                            double charge = v.CalculateSHNCharges();
                            Console.WriteLine("Make Payment of {0}", charge);
                        }
                    }
                    te.IsPaid = true;
                }
            }
        }

        // Method to create and display menu
        
        static int DisplayMenu()
        {
            Console.WriteLine("---------------- MENU ----------------\n" +
                    "[1] List all visitors\n[2] List Person Details\n" +
                    "[3] Assign/Replace TraceTogether Token\n[4] List all Business Locations\n" +
                    "[5] Edit Business Location Capacity\n[6] SafeEntry Check-in\n" +
                    "[7] SafeEntry Check-out\n[8] List all SHN Facilities\n" +
                    "[9] Create Visitor\n [10] Create TravelEntry Record\n" +
                    "[11] Calculate SHN Charges\n" +
                    "[0] Exit\n-------------------------------------");
            Console.Write("Enter your option: ");
            int option = Convert.ToInt32(Console.ReadLine());
            return option;
        }
    }
}
