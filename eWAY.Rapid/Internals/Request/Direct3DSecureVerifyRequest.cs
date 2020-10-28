namespace eWAY.Rapid.Internals.Request
{
    internal class Direct3DSecureVerifyRequest : BaseRequest
    {
        public long TraceId { get; set; }
        public string AccessCode { get; set; }
    }
}
