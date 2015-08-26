using eWAY.Rapid.Internals.Enums;

namespace eWAY.Rapid.Internals.Request
{
    internal class SettlementReportRequest: BaseRequest
    {
        public string SettlementDate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public SettlementReportMode ReportMode { get; set; }
        public string CardType { get; set; }
        public string Currency { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
