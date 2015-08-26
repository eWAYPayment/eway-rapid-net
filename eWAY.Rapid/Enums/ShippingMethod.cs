namespace eWAY.Rapid.Enums
{
    /// <summary>
    /// Defines the possible types of shiping used for a transaction
    /// </summary>
    public enum ShippingMethod
    {
        /// <summary>
        /// Method is unknown.
        /// </summary>
        Unknown,
        /// <summary>
        /// A low cost method is used
        /// </summary>
        LowCost,
        /// <summary>
        /// The customer has chosen the method
        /// </summary>
        DesignatedByCustomer,
        /// <summary>
        /// Item will be shipped international
        /// </summary>
        International,
        /// <summary>
        /// Item will be shipped via the military
        /// </summary>
        Military,
        /// <summary>
        /// Item will be delivered the next day
        /// </summary>
        NextDay,
        /// <summary>
        /// Item will be picked up from a physical location
        /// </summary>
        StorePickup,
        /// <summary>
        /// Item will be delivered in two days
        /// </summary>
        TwoDayService,
        /// <summary>
        /// Item will be delivered in three days
        /// </summary>
        ThreeDayService,
        /// <summary>
        /// A shipping method not listed
        /// </summary>
        Other
    }
}
