using System.Collections.Generic;

namespace eWAY.Rapid.Models
{
    /// <summary>
    /// Enroll request for 3D Secure
    /// </summary>
    public class Direct3DSEnrollRequest
    {
        ///<summary>Contains members that define a Rapid token customer (and card) stored in the merchant's account.</summary>
        public Customer Customer { get; set; }
        /// <summary>Customer's Shipping address.</summary>
        public ShippingAddress ShippingAddress { get; set; }
        /// <summary>List of Line items for this transaction.</summary>
        public List<LineItem> Items { get; set; }
        /// <summary>Payment details</summary>
        public PaymentDetails Payment { get; set; }
        /// <summary>The url that will redirect to after enrolling.</summary>
        public string RedirectUrl { get; set; }
    }
}
