using eWAY.Rapid.Internals.Models;

namespace eWAY.Rapid.Internals.Response
{
    internal class DirectCustomerResponse: BaseResponse
    {
        public DirectTokenCustomer Customer { get; set; }
    }
}
