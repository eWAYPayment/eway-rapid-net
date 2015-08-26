namespace eWAY.Rapid.Models
{
    /// <summary>
    /// LineItem
    /// </summary>
    public class LineItem
    {
        /// <summary>
        /// ID of the Line Item's product
        /// </summary>
        public string SKU { get; set; }
        /// <summary>
        /// Product description of the item
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The number of items
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Price (in cents) of each item
        /// </summary>
        public int UnitCost { get; set; }
        /// <summary>
        /// Combined tax (in cents) for all the items
        /// </summary>
        public int Tax { get; set; }
        /// <summary>
        /// Total in cents (including Tax) for all the items.
        /// </summary>
        public int Total { get; set; }
    }
}
