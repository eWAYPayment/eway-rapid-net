using eWAY.Rapid.Enums;

namespace eWAY.Rapid.Models
{
    /// <summary>
    /// This Response is returned by the Settlement Search Method. 
    /// It wraps the balance per card summary for a settlement summary
    /// </summary>
    public class BalanceSummaryPerCardType
    {
        /// <summary>
        /// The code of the card type of this balance
        /// </summary>
        public CardType CardType { get; set; }
        /// <summary>
        /// The number of transactions for this balance
        /// </summary>
        public int NumberOfTransactions { get; set; }
        /// <summary>
        /// The total amount credited in the settlement in cents
        /// </summary>
        public int Credit { get; set; }
        /// <summary>
        /// The total amount debited in this settlement in cents
        /// </summary>
        public int Debit { get; set; }
        /// <summary>
        /// The total balance settled in this settlement in cents 
        /// </summary>
        public int Balance { get; set; }
    }
}
