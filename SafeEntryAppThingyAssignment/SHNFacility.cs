using System;
using System.Collections.Generic;
using System.Text;

namespace SafeEntryAppThingyAssignment
{
    class SHNFacility
    {

        //Class Properties
        public string FacilityName { get; set; }

        public int FacilityCapacity { get; set; }

        public int FacilityVacancy { get; set; }

        public double DistFromAirCheckpoint { get; set; }

        public double DistFromSeaCheckpoint { get; set; }

        public double DistFromLandCheckpoint { get; set; }

        public SHNFacility()
        {

        }

        //Class Constructor
        //*Note Vacancy is not included in the list as it does not appear in the json api
        //Vacancy is also only calculated via capacity minus the no. of people staying there instead

        public SHNFacility(string facilityName, int facilityCapacity,  double disFromAirCheckpoint,
            double distFromSeaCheckpoint, double distFromLandCheckpoint)
        {
            FacilityName = facilityName;
            FacilityCapacity = facilityCapacity;
            DistFromAirCheckpoint = disFromAirCheckpoint;
            DistFromSeaCheckpoint = distFromSeaCheckpoint;
            DistFromLandCheckpoint = distFromLandCheckpoint;
        }

        //Method to Calculate Travel cost incurred
        //Will check timing of travel entry before checking how user is entering from
        // their specific checkpoints

        public double CalculateTravelCost(string entryMode, DateTime entryDate)
        {
            if (entryDate.Hour >=6 && entryDate.Hour <9 || entryDate.Hour >=18 && entryDate.Hour <0)
            {
                if(entryMode == "Land")
                {
                    return ((50 + (DistFromLandCheckpoint) * 0.22)*1.25)*1.07;
                }
                else if (entryMode == "Sea")
                {
                    return ((50 + (DistFromSeaCheckpoint) * 0.22)*1.25)*1.07;
                }
                else
                {
                    return ((50 + (DistFromAirCheckpoint) * 0.22)*1.25)*1.07;
                }
            }
            else if (entryDate.Hour >= 0 && entryDate.Hour < 6)
            {
                if (entryMode == "Land")
                {
                    return ((50 + (DistFromLandCheckpoint) * 0.22) * 1.50)*1.07;
                }
                else if (entryMode == "Sea")
                {
                    return ((50 + (DistFromSeaCheckpoint) * 0.22) * 1.50)*1.07;
                }
                else
                {
                    return ((50 + (DistFromAirCheckpoint) * 0.22) * 1.50)*1.07;
                }
            }
            else
            {
                if (entryMode == "Land")
                {
                    return ((50 + (DistFromLandCheckpoint) * 0.22))*1.07;
                }
                else if (entryMode == "Sea")
                {
                    return ((50 + (DistFromSeaCheckpoint) * 0.22))*1.07;
                }
                else
                {
                    return ((50 + (DistFromAirCheckpoint) * 0.22))*1.07;
                }
            }
        }

        //To check if SHN Facility has vacancy (True) or not (False)

        public bool IsAvailable()
        {
            if (FacilityVacancy>=1)
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
            return "Facility Name: " + FacilityName + "\tFacility Capacity: " + FacilityCapacity +
                "\tFacility Vacancy:" + FacilityVacancy +
                "\nDistance from Air Checkpoint: " + DistFromAirCheckpoint + 
                "\nDistance from Sea Checkpoint: " + DistFromSeaCheckpoint + 
                "\nDistance from Land Checkpoint: " + DistFromLandCheckpoint;
        }
    }
}
