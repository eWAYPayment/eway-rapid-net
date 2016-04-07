using eWAY.Rapid.Internals.Models;

namespace eWAY.Rapid.Internals.Response
{
    internal class CreateAccessCodeResponse: BaseResponse
    {
        public string AccessCode { get;set; }
        public TokenCustomer Customer { get;set; }
        public Payment Payment { get;set; }
        public string FormActionURL { get;set; }
        public string CompleteCheckoutURL { get; set; }
        public string AmexECEncryptedData { get; set; }
    }
}
