using System;
using System.Collections.Generic;
using System.Text;

namespace SafeEntryAppThingyAssignment
{

    //https://covidmonitoringapiprg2.azurewebsites.net/facility 

    class SHNFacility
    {
        public string FacilityName { get; set; }

        public int FacilityCapacity { get; set; }

        public int FacilityVacancy { get; set; }

        public double DistFromAirCheckpoint { get; set; }

        public double DistFromSeaCheckpoint { get; set; }

        public double DistFromLandCheckpoint { get; set; }

        public SHNFacility()
        {

        }

        public SHNFacility(string facilityName, int facilityCapacity,  double disFromAirCheckpoint,
            double distFromSeaCheckpoint, double distFromLandCheckpoint)
        {
            FacilityName = facilityName;
            FacilityCapacity = facilityCapacity;
            DistFromAirCheckpoint = disFromAirCheckpoint;
            DistFromSeaCheckpoint = distFromSeaCheckpoint;
            DistFromLandCheckpoint = distFromLandCheckpoint;
        }

        //double,string,datetime

        public double CalculateTravelCost(string entryMode, DateTime entryDate)
        {
            if (entryDate.Hour >=6 && entryDate.Hour <9 || entryDate.Hour >=18 && entryDate.Hour <0)
            {
                if(entryMode == "Land")
                {
                    return (50 + (DistFromLandCheckpoint) * 0.22)*1.25;
                }
                else if (entryMode == "Sea")
                {
                    return (50 + (DistFromSeaCheckpoint) * 0.22)*1.25;
                }
                else
                {
                    return (50 + (DistFromAirCheckpoint) * 0.22)*1.25;
                }
            }
            else if (entryDate.Hour >= 0 && entryDate.Hour < 6)
            {
                if (entryMode == "Land")
                {
                    return (50 + (DistFromLandCheckpoint) * 0.22) * 1.50;
                }
                else if (entryMode == "Sea")
                {
                    return (50 + (DistFromSeaCheckpoint) * 0.22) * 1.50;
                }
                else
                {
                    return (50 + (DistFromAirCheckpoint) * 0.22) * 1.50;
                }
            }
            else
            {
                if (entryMode == "Land")
                {
                    return (50 + (DistFromLandCheckpoint) * 0.22);
                }
                else if (entryMode == "Sea")
                {
                    return (50 + (DistFromSeaCheckpoint) * 0.22);
                }
                else
                {
                    return (50 + (DistFromAirCheckpoint) * 0.22);
                }
            }
        }

        //bool 

        public void IsAvailable()
        {

        }

        public override string ToString()
        {
            return base.ToString() + "\tFacility Name: " + FacilityName + "\tFacility Capacity: " + FacilityCapacity + "\tFacility Vacancy: " 
                + FacilityVacancy + "\tDistance from Air Checkpoint: " + DistFromAirCheckpoint + "\tDistance from Sea Checkpoint: " 
                + DistFromSeaCheckpoint + "\tDistance from Land Checkpoint: " + DistFromLandCheckpoint;
        }
    }
}
