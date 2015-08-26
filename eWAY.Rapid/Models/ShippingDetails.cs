using eWAY.Rapid.Enums;

namespace eWAY.Rapid.Models
{
    /// <summary>
    /// Combines all the Shipping related information for a transaction
    /// </summary>
    public class ShippingDetails
    {
        /// <summary>
        /// First name on the shipping manifest
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last name on the shipping manifest
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// ShippingMethod enum.
        /// </summary>
        public ShippingMethod ShippingMethod { get; set; }
        /// <summary>
        /// Destination of the sale
        /// </summary>
        public Address ShippingAddress { get; set; }
        /// <summary>
        /// Email of the recipient
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Phone number of the recipient
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Fax number of the recipient
        /// </summary>
        public string Fax { get; set; }
    }
}
