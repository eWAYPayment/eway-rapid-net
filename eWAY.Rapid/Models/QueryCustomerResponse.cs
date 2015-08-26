namespace eWAY.Rapid.Models
{
    /// <summary>
    /// Query Customer Response
    /// </summary>
    public class QueryCustomerResponse: BaseResponse
    {
        /// <summary>
        /// List of customers returned.
        /// </summary>
        public Customer[] Customers { get; set; }
    }
}
