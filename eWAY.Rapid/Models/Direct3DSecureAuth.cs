namespace eWAY.Rapid.Models
{
    /// <summary>
    /// 3D Secure verify response
    /// </summary>
    public class Direct3DSecureAuth
    {
        /// <summary>Cardholder Authentication Value</summary>
        public string Cryptogram { get; set; }
        /// <summary>Electronic Commerce Indicator</summary>
        public string ECI { get; set; }
        /// <summary>Transaction identifier resulting from authentication processing for 3DS V1</summary>
        public string XID { get; set; }
        /// <summary>The result of the 3D Secure authentication </summary>
        public string AuthStatus { get; set; }
        /// <summary>Version of 3D Secure.</summary>
        public string Version { get; set; }
        /// <summary>Transaction Id for 3DS 2.0</summary>
        public string dsTransactionId { get; set; }
    }
}
