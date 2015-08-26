using eWAY.Rapid.Models;

namespace eWAY.Rapid.Internals.Request
{
    internal class TransactionSearchRequest: BaseRequest
    {
        public TransactionFilter TransactionFilter { get; set; }
    }
}
