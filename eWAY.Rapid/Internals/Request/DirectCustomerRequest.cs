using eWAY.Rapid.Internals.Models;

namespace eWAY.Rapid.Internals.Request
{
    internal class DirectCustomerRequest : BaseRequest
    {
        public DirectTokenCustomer Customer { get; set; }
        public string ThirdPartyWalletID { get; set; }
    }
}
