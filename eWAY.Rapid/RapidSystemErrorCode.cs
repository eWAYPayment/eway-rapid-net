namespace eWAY.Rapid
{
    /// <summary>
    /// Contains system error codes for the eWAY Rapid library.
    /// </summary>
    public static class RapidSystemErrorCode
    {
        /// <summary>
        /// Invalid endpoint error.
        /// </summary>
        public const string INVALID_ENDPOINT_ERROR = "S9990";
        /// <summary>
        /// Invalid credential error.
        /// </summary>
        public const string INVALID_CREDENTIAL_ERROR = "S9991";
        /// <summary>
        /// Communication error.
        /// </summary>
        public const string COMMUNICATION_ERROR = "S9992";
        /// <summary>
        /// Authentication error.
        /// </summary>
        public const string AUTHENTICATION_ERROR = "S9993";
        /// <summary>
        /// Internal SDK error.
        /// </summary>
        public const string INTERNAL_SDK_ERROR = "S9995";
    }
}
