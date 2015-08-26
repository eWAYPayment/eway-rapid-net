namespace eWAY.Rapid.Internals.Models
{
    internal class SettlementSummary
    {
        public string SettlementID { get; set; }
        public string Currency { get; set; }
        public int TotalCredit { get; set; }
        public int TotalDebit { get; set; }
        public int TotalBalance { get; set; }
        public BalanceSummaryPerCardType[] BalancePerCardType { get; set; }
    }
}
