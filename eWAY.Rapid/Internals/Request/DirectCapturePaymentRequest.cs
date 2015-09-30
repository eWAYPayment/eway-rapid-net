using eWAY.Rapid.Internals.Models;

namespace eWAY.Rapid.Internals.Request
{
    internal class DirectCapturePaymentRequest: CaptureAuthBaseRequest
    {
        public Payment Payment { get; set; }
    }
}
