using eWAY.Rapid.Internals.Enums;

namespace eWAY.Rapid.Internals.Request
{
    internal class CreateAccessCodeSharedRequest: CreateAccessCodeRequest
    {
        public CreateAccessCodeSharedRequest()
        {
            this.AllowedCards = AllowedCards.None;
        }

        public bool? CustomerReadOnly { get;set; }
        public bool? VerifyCustomerPhone { get;set; }
        public bool? VerifyCustomerEmail { get;set; }
        public string ReturnUrl { get;set; }
        public string LogoUrl { get;set; }
        public string FooterText { get;set; }
        public string HeaderText { get;set; }
        public string Language { get;set; }
        public AllowedCards AllowedCards { get;set; }
        public CustomView? CustomView { get; set; }
    }
}
