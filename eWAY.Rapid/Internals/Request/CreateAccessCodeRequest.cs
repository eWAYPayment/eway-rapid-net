using eWAY.Rapid.Internals.Enums;
using eWAY.Rapid.Internals.Models;

namespace eWAY.Rapid.Internals.Request
{
    internal class CreateAccessCodeRequest : BaseRequest
    {
        public CreateAccessCodeRequest()
        {
            this.CheckoutUrl = "";
            this.TransactionType = TransactionTypes.Purchase;
            this.PaymentType = PaymentType.None;
            this.CheckoutPayment = false;
        }

        public Customer Customer { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public LineItem[] Items { get; set; }
        public Option[] Options { get; set; }
        public Payment Payment { get; set; }
        public string RedirectUrl { get; set; }
        public string CancelUrl { get; set; }
        public string CheckoutUrl { get; set; }
        public string CustomerIP { get; set; }
        public string DeviceID { get; set; }
        public string PartnerID { get; set; }
        public Method Method { get; set; }
        public TransactionTypes TransactionType { get; set; }
        public PaymentType PaymentType { get; set; }
        public bool CheckoutPayment { get; set; }
    }
}
