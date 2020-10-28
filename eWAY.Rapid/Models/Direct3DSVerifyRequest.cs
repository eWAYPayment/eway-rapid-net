namespace eWAY.Rapid.Models
{
    /// <summary>
    /// 3DS verify request.
    /// </summary>
    public class Direct3DSVerifyRequest
    {
        /// <summary>The traceId returned by invoking 3ds enroll.</summary>
        public long TraceId { get; set; }
        /// <summary>The AccessCode returned by invoking 3ds enroll.</summary>
        public string AccessCode { get; set; }
    }
}
