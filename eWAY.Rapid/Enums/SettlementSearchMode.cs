namespace eWAY.Rapid.Enums
{
    /// <summary>
    /// Defines the possible types of settlement reports
    /// </summary>
    public enum SettlementSearchMode
    {
        /// <summary>
        /// This mode will ONLY query the settlement summary.
        /// </summary>
        SummaryOnly,
        /// <summary>
        /// This mode will ONLY query the settlement transactions (individually).
        /// </summary>
        TransactionOnly,
        /// <summary>
        /// This mode will query both the Settlement summary, as well as the settlement transactions.
        /// </summary>
        Both,
    }
}
