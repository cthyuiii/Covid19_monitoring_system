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
            foreach (string s in csvLines)
            {
                string[] data = s.Split(',');
                string businessName = data[0];
                string branchCode = data[1];
                int maximumCapcity = Convert.ToInt32(data[2]);
            }

        }

        //Method to Load Person data from csv file as well as adding data into it

        static void InitDataPerson(List<Person> pList)
        {
            string[] csvLines = File.ReadAllLines("Person.csv");
            foreach (string s in csvLines)
            {
                string[] data = s.Split(',');
                string type = data[0];
                string name = data[1];
                string address = data[2];
                DateTime lastLeftCountry = Convert.ToDateTime(data[3]);
                string passportNo = data[4];
                string nationality = data[5];
                string tokenSerial = data[6];
                string tokenCollectionLocation = data[7];
                DateTime tokenExpiryDate = Convert.ToDateTime(data[8]);
                string travelEntryLastCountry = data[9];
                string travelEntryMode = data[10];
                DateTime travelEntryDate = Convert.ToDateTime(data[11]);
                DateTime travelShnEndDate = Convert.ToDateTime(data[12]);
                bool travelIsPaid = Convert.ToBoolean(data[13]);
                string facilityName = data[14];
            }
        }

        //3) Method to List all Visitors
        static void DisplayVisitors(List<Person> pList)
        {
            if (Person is Visitor)
            {

            }

        }

        /*4) Method to List Person Details
            1. prompt user for name
            2. search for person
            3. list person details including TravelEntry and SafeEntry details
            i. if resident, display TraceTogetherToken details
        */

        static void DisplayPerson()
        {

        }

        /*5) Method to Assign/Replace TraceTogether Token
            1. prompt user for name
            2. search for resident name
            3. create and assign a TraceTogetherToken object if resident has no existing token
            4. replace token if it meets the criteria stipulated in the background brief
        */

        static void AssignReplaceToken()
        {

        }

        //6) Method to List all Business Locations
        static void DisplayBusinessLocation()
        {

        }

        /*7) Method to Edit Business Location Capacity
            1. prompt user to enter details
            2. search for business location
            3. prompt user to edit maximum capacity
        */

        static void EditBusinessLocationCapacity()
        {

        }

        /*8) Method for SafeEntry Check-in
            1. prompt user for name
            2. search for person
            3. list all business locations
            4. prompt user to select for business location to check-in
            5. create SafeEntry object if the location is not full, and increase visitorsNow count
            6. add SafeEntry object to person
        */

        static void SafeEntryCheckin()
        {

        }

        /*9) Method for SafeEntry Check-out
            1. prompt user for name
            2. search for person
            3. list SafeEntry records for that person that have not been checked-out
            4. prompt user to select record to check-out
            5. call PerformCheckOut() to check-out, and reduce visitorsNow by 1
        */

        static void SafeEntryCheckout()
        {

        }

        //10) Method to List all SHN Facilities
        static void DisplaySHNFacilities()
        {

        }

        /*11) Method to Create Visitor Object
            1. prompt user for details
            2. create Visitor object
        */

        static void CreateVisitor()
        {

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

        static void CreateTravelEntry()
        {

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

        static void CheckSHNCharges()
        {

        }

    }
}
