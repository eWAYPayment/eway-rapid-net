using System;

namespace eWAY.Rapid.Enums
{
    /// <summary>
    /// This defines the type of the transaction, it is a very close mapping to the bank accepted types, 
    /// note the types Refund and Auth are missing as they are handled using dedicated requests.
    /// </summary>
    public enum TransactionTypes
    {
        /// <summary>
        /// Unkonwn transaction type
        /// </summary>
        Unknown,
        /// <summary>
        /// Used for a single purchase where the card is present. This will require that the CVN details are supplied.
        /// </summary>
        Purchase,
        /// <summary>
        /// Used for a recurring transaction where the card details have been stored. This transaction type should be used when charging with a Token Customer
        /// </summary>
        Recurring,
        /// <summary>
        /// Mail order or Telephone Transaction. Used when the card is not at hand. Can also be used when charging a Token customer.
        /// </summary>
        MOTO
    }  
}
