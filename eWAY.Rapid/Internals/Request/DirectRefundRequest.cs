using eWAY.Rapid.Internals.Models;

namespace eWAY.Rapid.Internals.Request
{
    internal class DirectRefundRequest: BaseRequest
    {
        public Refund Refund { get;set; }
        public DirectTokenCustomer Customer { get;set; }
        public ShippingAddress ShippingAddress { get;set; }
        public LineItem[] Items { get;set; }
        public Option[] Options { get;set; }
        public string CustomerIP { get;set; }
        public string DeviceID { get;set; }
        public string PartnerID { get; set; }
    }
}
