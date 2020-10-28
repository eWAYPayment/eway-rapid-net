namespace eWAY.Rapid.Models
{
    /// <summary>
    /// 3DS verify response.
    /// </summary>
    public class Direct3DSVerifyResponse : BaseResponse
    {
        /// <summary></summary>
        public string TraceId { get; set; }
        public string AccessCode { get; set; }
        public Direct3DSecureAuth ThreeDSecureAuth { get; set; }
    }
}
