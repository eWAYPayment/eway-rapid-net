namespace eWAY.Rapid.Models
{
    /// <summary>
    /// Address
    /// </summary>
    public class Address
    {
        /// <summary>
        /// First line of the street address. e.g. "Unit 1"
        /// </summary>
        public string Street1 { get; set; }
        /// <summary>
        /// Second line of the street address. e.g. "6 Coonabmble st"
        /// </summary>
        public string Street2 { get; set; }
        /// <summary>
        /// City for the address, e.g. "Gulargambone"
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// State or province code. e.g. 'NSW"
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// Two character Country Code. e.g. "AU"
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// e.g. 2828
        /// </summary>
        public string PostalCode { get; set; }
    }
}
