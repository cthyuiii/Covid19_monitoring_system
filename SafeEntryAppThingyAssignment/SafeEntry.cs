using System;
using System.Collections.Generic;
using System.Text;

namespace SafeEntryAppThingyAssignment
{

    class SafeEntry
    {

        //Class Properties

        public  DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public BusinessLocation Location { get; set; }

        public SafeEntry()
        {

        }

        //Class Constructor

        public SafeEntry(DateTime checkIn, BusinessLocation location)
        {
            CheckIn = checkIn;
            Location = location;
        }

        //Method to call once a user performs a checkout
        //It simply reduces the number of visitors at a location by 1

        public void PerformCheckOut()
        {
            Location.VisitorsNow = Location.VisitorsNow - 1;
        }

        //To string method

        public override string ToString()
        {
            return "Check In Time: " + CheckIn + "   \tCheck Out Time: " + CheckOut + "   \tLocation: " + Location;
        }
    }
}
