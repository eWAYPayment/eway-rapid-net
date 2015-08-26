using eWAY.Rapid.Internals.Models;

namespace eWAY.Rapid.Internals.Request
{
    internal class CapturePaymentRequest: CaptureAuthBaseRequest
    {
        public Payment Payment { get; set; }
    }
}
