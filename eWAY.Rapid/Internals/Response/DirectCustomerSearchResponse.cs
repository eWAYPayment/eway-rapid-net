using eWAY.Rapid.Internals.Models;

namespace eWAY.Rapid.Internals.Response
{
    internal class DirectCustomerSearchResponse: BaseResponse
    {
        public DirectTokenCustomer[] Customers { get; set; }
    }
}
