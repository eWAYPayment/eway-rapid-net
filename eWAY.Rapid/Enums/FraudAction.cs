namespace eWAY.Rapid.Enums
{
    /// <summary>
    /// Defines the possible actions that may have been taken when/if an anti-fraud rule on the account has been triggered.
    /// </summary>
    public enum FraudAction
    {
        /// <summary>
        /// Normal Transaction
        /// </summary>
        NotChallenged,
        /// <summary>
        /// Transaction was allowed
        /// </summary>
        Allow,
        /// <summary>
        /// Transaction was held for review
        /// </summary>
        Review,
        /// <summary>
        /// Transaction was held for review and pre-authed	
        /// </summary>
        PreAuth,
        /// <summary>
        /// Transaction was processed
        /// </summary>
        Processed,
        /// <summary>
        /// Transaction was approved when reviewed
        /// </summary>
        Approved,
        /// <summary>
        /// Transaction was not approved when reviewed
        /// </summary>
        Block
    }
}
