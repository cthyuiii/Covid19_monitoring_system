using System;
using System.Collections.Generic;
using System.Text;

namespace SafeEntryAppThingyAssignment
{
    class BusinessLocation
    {
        //Class Properties

        public string BusinessName { get; set; }

        public string BranchCode { get; set; }

        public int MaximumCapacity { get; set; }

        public int VisitorsNow { get; set; }

        public BusinessLocation()
        {

        }

        //Class Constructor

        public BusinessLocation(string businessName, string branchCode, int maximumCapcity, int visitorsNow)
        {
            BusinessName = businessName;
            BranchCode = branchCode;
            MaximumCapacity = maximumCapcity;
            VisitorsNow = visitorsNow;
        }

        //Method to check if Location is at max capacity (true) or not (false)

        public bool IsFull()
        {
            if (MaximumCapacity == VisitorsNow)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //To string method

        public override string ToString()
        {
            return base.ToString() + "\tBusiness Name: " + BusinessName + "\tBranch Code: " + BranchCode + "\tMax Capacity: "
                + MaximumCapacity + "\tNo of Visitors: " + VisitorsNow ;
        }
    }
}
