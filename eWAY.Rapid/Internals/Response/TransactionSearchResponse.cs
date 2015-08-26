using eWAY.Rapid.Internals.Models;

namespace eWAY.Rapid.Internals.Response
{
    internal class TransactionSearchResponse: BaseResponse
    {
        public TransactionResult[] Transactions { get; set; }
    }
}
