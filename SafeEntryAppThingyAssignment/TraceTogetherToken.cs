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

        public bool IsEligibleForReplacement()
        {
            if (DateTime.Today >= ExpiryDate && (DateTime.Today).AddMonths(1) <= (ExpiryDate).AddMonths(1) )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ReplaceToken(string serialNo, string collectionLocation)
        {
            SerialNo = serialNo;
            CollectionLocation = collectionLocation;
        }

        public override string ToString()
        {
            return base.ToString() + "\tSerial No.: " + "\tCollection Location: " + CollectionLocation + "\tExpiry Date: " + ExpiryDate;
        }
    }
}
