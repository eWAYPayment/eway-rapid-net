using eWAY.Rapid.Internals.Models;

namespace eWAY.Rapid.Internals.Response
{
    internal class Direct3DSecureVerifyResponse : BaseResponse
    {
        public string TraceId { get; set; }
        public string AccessCode { get; set; }
        public Direct3DSecureAuth ThreeDSecureAuth { get; set; }
    }
}
