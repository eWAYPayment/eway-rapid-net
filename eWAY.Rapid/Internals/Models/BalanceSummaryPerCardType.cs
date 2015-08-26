namespace eWAY.Rapid.Internals.Models
{
    internal class BalanceSummaryPerCardType
    {
        public string CardType { get; set; }
        public int NumberOfTransactions { get; set; }
        public int Credit { get; set; }
        public int Debit { get; set; }
        public int Balance { get; set; }
    }
}
