﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SafeEntryAppThingyAssignment
{
    class BusinessLocation
    {
        public string BusinessName { get; set; }

        public string BranchCode { get; set; }

        public int MaximumCapacity { get; set; }

        public int VisitorsNow { get; set; }

        public BusinessLocation()
        {

        }

        public BusinessLocation(string businessName, string branchCode, int maximumCapcity, int visitorsNow)
        {
            BusinessName = businessName;
            BranchCode = branchCode;
            MaximumCapacity = maximumCapcity;
            VisitorsNow = visitorsNow;
        }

        // bool 

        public void IsFull()
        {

        }

        public override string ToString()
        {
            return base.ToString() + "Business Name: " + BusinessName + "Branch Code: " + BranchCode + "Max Capacity: "
                + MaximumCapacity + "No of Visitors: " + VisitorsNow ;
        }
    }
}
