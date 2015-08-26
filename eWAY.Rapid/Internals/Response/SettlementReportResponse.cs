using eWAY.Rapid.Internals.Models;

namespace eWAY.Rapid.Internals.Response
{
    internal class SettlementReportResponse: BaseResponse
    {
        public SettlementSummary[] SettlementSummaries { get;set; }
        public SettlementTransaction[] SettlementTransactions { get; set; }
    }
}
