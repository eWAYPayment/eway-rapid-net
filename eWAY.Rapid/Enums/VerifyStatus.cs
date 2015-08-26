namespace eWAY.Rapid.Enums
{
    /// <summary>
    /// Possible values returned from the payment providers with regards to verification of card/user details.
    /// </summary>
    public enum VerifyStatus
    {
        ///<summary>No verification</summary>
        Unchecked,
        ///<summary>Verification succeeded</summary>
        Valid,
        ///<summary>Verification failed</summary>
        Invalid
    }
}
