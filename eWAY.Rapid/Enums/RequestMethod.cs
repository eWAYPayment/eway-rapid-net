namespace eWAY.Rapid.Enums
{
    /// <summary>
    /// Request Method
    /// </summary>
    public enum RequestMethod
    {
        ///<summary>Process a standard payment</summary>
        ProcessPayment,
        ///<summary>Create a token customer</summary>
        CreateTokenCustomer,
        ///<summary>Update a token customer</summary>
        UpdateTokenCustomer,
        ///<summary>Process a payment with a Token Customer</summary>
        TokenPayment,
        ///<summary>Process a pre-auth transaction - funds are only held from the customer's card</summary>
        Authorise
    }
}
