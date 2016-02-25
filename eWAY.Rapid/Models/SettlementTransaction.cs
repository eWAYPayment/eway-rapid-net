using System;
using eWAY.Rapid.Enums;

namespace eWAY.Rapid.Models
{
    /// <summary>
    /// This Response is returned by the Settlement Search Method. 
    /// It wraps the settlement transactions
    /// </summary>
    public class SettlementTransaction
    {
        /// <summary>
        /// The unique ID of the settlement
        /// </summary>
        public string SettlementID { get; set; }
        /// <summary>
        /// The eWAY Customer ID associated with this settlement transaction
        /// </summary>
        public int eWAYCustomerID { get; set; }
        /// <summary>
        /// The numeric code for the currency of the settlement
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// The unique eWAY Transaction ID associated with this settlement transaction
        /// </summary>
        public int TransactionID { get; set; }
        /// <summary>
        /// The unique Transaction ID as returned from the bank
        /// </summary>
        public string TxnReference { get; set; }
        /// <summary>
        /// The code of the card type of this settlement
        /// </summary>
        public CardType CardType { get; set; }
        /// <summary>
        /// The amount of the settlement transaction in cents
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// A numeric representation of the transaction type
        /// </summary>
        public string TransactionType { get; set; }
        /// <summary>
        /// The GMT date of the transaction 
        /// </summary>
        public DateTime TransactionDateTime { get; set; }
        /// <summary>
        /// The GMT date of settlement
        /// </summary>
        public DateTime SettlementDateTime { get; set; }
    }
}
