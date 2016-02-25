namespace eWAY.Rapid.Models
{
    /// <summary>
    /// This Response is returned by the Settlement Search Method. 
    /// It contains the settlment summaries and/or the settlement transactions requested
    /// </summary>
    public class SettlementSearchResponse: BaseResponse
    {
        /// <summary>
        /// The daily summary of transactions in the settlement search
        /// </summary>
        public SettlementSummary[] SettlementSummaries { get;set; }
        /// <summary>
        /// The details of each transaction in the settlement search
        /// </summary>
        public SettlementTransaction[] SettlementTransactions { get; set; }
    }
}
