namespace eWAY.Rapid.Models
{
    /// <summary>
    /// This Response is returned by the Settlement Search Method. 
    /// It wraps the settlement summary
    /// </summary>
    public class SettlementSummary
    {
        /// <summary>
        /// The unique ID of the settlement
        /// </summary>
        public string SettlementID { get; set; }
        /// <summary>
        /// The numeric code for the currency of the settlement
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// The total amount credited in the settlement in cents
        /// </summary>
        public int TotalCredit { get; set; }
        /// <summary>
        /// The total amount debited in this settlement in cents
        /// </summary>
        public int TotalDebit { get; set; }
        /// <summary>
        /// The total balance settled in this settlement in cents
        /// </summary>
        public int TotalBalance { get; set; }
        /// <summary>
        /// The summary of balances settled for each card type
        /// </summary>
        public BalanceSummaryPerCardType[] BalancePerCardType { get; set; }
    }
}
