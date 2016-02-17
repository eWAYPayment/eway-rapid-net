namespace eWAY.Rapid.Internals.Models
{
    internal class Customer
    {
        public long? TokenCustomerID { get;set; }
        public string Reference { get;set; }
        public string Title { get;set; }
        public string FirstName { get;set; }
        public string LastName { get;set; }
        public string CompanyName { get;set; }  
        public string JobDescription { get;set; }
        public string Street1 { get;set; }
        public string Street2 { get;set; }
        public string City { get;set; }
        public string State { get;set; }
        public string PostalCode { get;set; }
        public string Country { get;set; }
        public string Email { get;set; }
        public string Phone { get;set; }
        public string Mobile { get;set; }
        public string Comments { get;set; }
        public string Fax { get;set; }
        public string Url { get; set; }

        public string getTokenCustomerID()
        {
            return TokenCustomerID.ToString();
        }

    }
}
