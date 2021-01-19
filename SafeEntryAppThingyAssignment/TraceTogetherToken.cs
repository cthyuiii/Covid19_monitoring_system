using System;
using System.Collections.Generic;
using System.Text;

namespace SafeEntryAppThingyAssignment
{
    class TraceTogetherToken
    {
        public string SerialNo { get; set; }

        public string CollectionLocation { get; set; }

        public DateTime ExpiryDate { get; set; }

        public TraceTogetherToken()
        {

        }

        public TraceTogetherToken(string serialNo, string collectionLocation, DateTime expiryDate)
        {
            SerialNo = serialNo;
            CollectionLocation = collectionLocation;
            ExpiryDate = expiryDate;
        }

        //incomplete bool

        public void IsEligibleForReplacement()
        {

        }

        public void ReplaceToken(string serialNo, string collectionLocation)
        {
            SerialNo = serialNo;
            CollectionLocation = collectionLocation;
        }
    }
}
