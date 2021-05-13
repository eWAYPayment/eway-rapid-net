namespace eWAY.Rapid.Internals.Response
{
    internal class Direct3DSecureEnrollResponse : BaseResponse
    {
        public string Default3dsUrl { get; set; }
        public long TraceId { get; set; }
        public string AccessCode { get; set; }
    }
}
