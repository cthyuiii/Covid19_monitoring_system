using System;
using System.Collections.Generic;
using System.Text;

namespace SafeEntryAppThingyAssignment
{
    class TraceTogetherToken
    {

        //Class Properties
        public string SerialNo { get; set; }

        public string CollectionLocation { get; set; }

        public DateTime ExpiryDate { get; set; }

        public TraceTogetherToken()
        {

        }

        //Class Constructor

        public TraceTogetherToken(string serialNo, string collectionLocation, DateTime expiryDate)
        {
            SerialNo = serialNo;
            CollectionLocation = collectionLocation;
            ExpiryDate = expiryDate;
        }

        //This method checks if user is still eligible for a token replacement
        //It simply checks if the current date has already past the expiry date
        //As well as whether or not has it been over a month since the expiry date
        //by adding one month to expiry as well as current date
        //If eligible return True
        //Else false

        public bool IsEligibleForReplacement()
        {
            if (DateTime.Today >= ExpiryDate || (DateTime.Today).AddMonths(1) >= ExpiryDate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Constructor for token replacement

        public void ReplaceToken(string serialNo, string collectionLocation)
        {
            SerialNo = serialNo;
            CollectionLocation = collectionLocation;
        }

        //To string method

        public override string ToString()
        {
            return "Serial No: " + SerialNo + "\tCollection Location: " + CollectionLocation + "\tExpiry Date: " + ExpiryDate;
        }
    }
}
