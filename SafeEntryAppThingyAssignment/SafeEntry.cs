using System;
using System.Collections.Generic;
using System.Text;

namespace SafeEntryAppThingyAssignment
{
    class SafeEntry
    {
        public  DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public BusinessLocation Location { get; set; }

        public SafeEntry()
        {

        }

        public SafeEntry(DateTime checkIn, DateTime checkOut, BusinessLocation location)
        {
            CheckIn = checkIn;
            CheckOut = checkOut;
            Location = location;
        }

        public void PerformCheckOut()
        {

        }

        public override string ToString()
        {
            return base.ToString() + "\tCheck In Time: " + CheckIn + "\tCheck Out Time: " + CheckOut + "\tLocation: " + Location;
        }
    }
}
