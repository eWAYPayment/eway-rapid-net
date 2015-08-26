using eWAY.Rapid.Enums;

namespace eWAY.Rapid.Models
{
    /// <summary>
    /// The ShippingAddress section is optional. It is used by Beagle Fraud Alerts (Enterprise) to calculate a risk score for this transaction.
    /// </summary>
    public class ShippingAddress
    {
        /// <summary>
        /// The first name of the person the order is shipped to.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// The last name of the person the order is shipped to.
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// The street address the order is shipped to.
        /// </summary>
        public string Street1 { get; set; }
        /// <summary>
        /// The street address of the shipping location.
        /// </summary>
        public string Street2 { get; set; }
        /// <summary>
        /// The customer’s shipping city / town / suburb.
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// The customer’s shipping state / county
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// The customer’s shipping country. This should be the two letter ISO 3166-1 alpha-2 code. This field must be lower case. e.g. Australia = au
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// The customer’s shipping post / zip code.
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// The customer’s shipping email address, which must be correctly formatted if present.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// The phone number of the person the order is shipped to.
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// The fax number of the shipping location.
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// The method used to ship the customer’s order. 
        /// One of: Unknown, LowCost, DesignatedByCustomer, International, Military, NextDay, StorePickup, TwoDayService, ThreeDayService, Other
        /// </summary>
        public string ShippingMethod { get; set; }
    }
}
