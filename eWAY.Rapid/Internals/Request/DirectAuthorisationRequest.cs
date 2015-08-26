using eWAY.Rapid.Internals.Enums;

namespace eWAY.Rapid.Internals.Request
{
    internal class DirectAuthorisationRequest: DirectPaymentRequest
    {
        public DirectAuthorisationRequest()
        {
            this.Method = Method.Authorise;
        }
    }
}
