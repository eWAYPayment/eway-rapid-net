using System;

namespace eWAY.Rapid.Internals.Models
{
    internal class SettlementTransaction
    {
        public string SettlementID { get; set; }
        public int eWAYCustomerID { get; set; }
        public string Currency { get; set; }
        public int TransactionID { get; set; }
        public string TxnReference { get; set; }
        public string CardType { get; set; }
        public int Amount { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public DateTime SettlementDateTime { get; set; }
    }
}
