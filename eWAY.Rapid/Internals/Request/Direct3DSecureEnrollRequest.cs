using eWAY.Rapid.Internals.Models;
using System.Collections.Generic;

namespace eWAY.Rapid.Internals.Request
{
    internal 
        class Direct3DSecureEnrollRequest : BaseRequest
    {
        public DirectTokenCustomer Customer { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public List<LineItem> Items { get; set; }
        public Payment Payment { get; set; }
        public string RedirectUrl { get; set; }
        public string SecuredCardData { get; set; }
    }
}
