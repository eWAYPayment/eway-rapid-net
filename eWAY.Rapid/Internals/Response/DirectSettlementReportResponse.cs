using eWAY.Rapid.Internals.Models;

namespace eWAY.Rapid.Internals.Response
{
    internal class DirectSettlementSearchResponse: BaseResponse
    {
        public SettlementSummary[] SettlementSummaries { get;set; }
        public SettlementTransaction[] SettlementTransactions { get; set; }
    }
}
