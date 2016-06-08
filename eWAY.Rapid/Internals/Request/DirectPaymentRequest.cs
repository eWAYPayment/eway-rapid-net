using eWAY.Rapid.Internals.Enums;
using eWAY.Rapid.Internals.Models;

namespace eWAY.Rapid.Internals.Request
{
    internal class DirectPaymentRequest: BaseRequest
    {
        public string CreditCardNumber { get; set; }
        public string CreditCardCVN { get; set; }
        public DirectTokenCustomer Customer { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public LineItem[] Items { get; set; }
        public Option[] Options { get; set; }
        public Payment Payment { get; set; }
        public string RedirectUrl { get; set; }
        public string CustomerIP { get; set; }
        public string DeviceID { get; set; }
        public string PartnerID { get; set; }
        public Method Method { get; set; }
        public TransactionTypes TransactionType { get; set; }
        public string ThirdPartyWalletID { get; set; }
        public string SecuredCardData { get; set; }
    } 
}
