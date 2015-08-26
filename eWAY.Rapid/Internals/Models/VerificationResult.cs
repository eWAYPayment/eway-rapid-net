using eWAY.Rapid.Enums;

namespace eWAY.Rapid.Internals.Models
{
    internal class VerificationResult
    {
        public VerifyStatus CVN { get; set; }
        public VerifyStatus Address { get; set; }
        public VerifyStatus Email { get; set; }
        public VerifyStatus Mobile { get; set; }
        public VerifyStatus Phone { get; set; }
        public BeagleVerifyStatus BeagleEmail { get; set; }
        public BeagleVerifyStatus BeaglePhone { get; set; }
    }
}
