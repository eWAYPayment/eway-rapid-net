namespace eWAY.Rapid.Models
{
    /// <summary>
    /// Direct 3D Secure response.
    /// </summary>
    public class Direct3DSEnrollResponse : BaseResponse
    {
        /// <summary>The default 3ds page if merchant doesn't want to use SDK in their checkout page directly.</summary>
        public string Default3dsUrl { get; set; }
        /// <summary>The traceId used for SDK if merchant want to use SDK in their checkout page.</summary>
        public long TraceId { get; set; }
        /// <summary>The accesscode used for SDK if merchant want to use SDK in their checkout page.</summary>
        public string AccessCode { get; set; }
    }
}
