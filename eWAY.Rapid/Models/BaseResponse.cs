using System.Collections.Generic;

namespace eWAY.Rapid.Models
{
    /// <summary>
    /// Base Response
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// List of all validation, processing, fraud or system errors that occurred when processing this request. 
        /// Null if no errors occured. This member combines all errors related to the request.
        /// </summary>
        public List<string> Errors { get; set; }
    }
}
