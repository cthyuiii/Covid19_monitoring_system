using System;
using System.Collections.Generic;
using System.Text;

namespace SafeEntryAppThingyAssignment
{
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

        //double

        public void CalculateTravelCost()
        {

        }

        //bool 

        public void IsAvailable()
        {

        }

        public override string ToString()
        {
            return base.ToString() + "Facility Name: " + FacilityName + "Facility Capacity: " + FacilityCapacity + "Facility Vacancy: " 
                + FacilityVacancy + "Distance from Air Checkpoint: " + DistFromAirCheckpoint + "Distance from Sea Checkpoint: " 
                + DistFromSeaCheckpoint + "Distance from Land Checkpoint: " + DistFromLandCheckpoint;
        }
    }
}
