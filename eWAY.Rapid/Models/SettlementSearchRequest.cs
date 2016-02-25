using eWAY.Rapid.Enums;

namespace eWAY.Rapid.Models
{
    /// <summary>
    /// Settlement search filter
    /// </summary>
    public class SettlementSearchRequest
    {
        /// <summary>
        /// The settlement date to be queried. 
        /// Note: if a date range is used, SettlementDate will be ignored
        /// This should be formatted as YYYY-MM-DD
        /// </summary>
        public string SettlementDate { get; set; }

        /// <summary>
        /// This parameter sets the start of a filtered date range. 
        /// Note: if a date range is used, SettlementDate will be ignored
        /// This should be formatted as YYYY-MM-DD
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// This parameter sets the end of a filtered date range. 
        /// Note: if a date range is used, SettlementDate will be ignored
        /// This should be formatted as YYYY-MM-DD
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// The type of settlement search report to return
        /// </summary>
        public SettlementSearchMode ReportMode { get; set; }

        /// <summary>
        /// The card type to filter the search by
        /// </summary>
        public CardType CardType { get; set; }

        /// <summary>
        /// The currency to filter the report by. The three digit ISO 4217 currency code 
        /// should be used or ALL for all currencies. 
        /// This should be in uppercase.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// The page number to retrieve
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// The number of records to retrieve per page
        /// </summary>
        public int PageSize { get; set; }
    }
}
