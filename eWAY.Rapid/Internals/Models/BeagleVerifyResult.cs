using eWAY.Rapid.Enums;

namespace eWAY.Rapid.Internals.Models
{
    internal class BeagleVerifyResult
    {
        public BeagleVerifyStatus Email { get; set; }
        public BeagleVerifyStatus Phone { get; set; }
    }
}
