# Changelog

All Notable changes will be documented in this file

## 1.6.3

 - Update the SDK API to support Rapid 3D Secure enroll and verify.

## 1.6.2

 - Change default TransactionType to MOTO when creating a Token Customer.

## 1.6.1

 - Fix missing card details for token customer reponses
 - Updated Automapper dependency to v6.2.2

## 1.6.0

 - Updated dependencies: Newtonsoft.Json to v10.0.3 and Automapper to v6.2.0 (thanks @rolandh)

## 1.5.3

 - Added `CancelURL` and `SecuredCardData` to `Customer` so that Tokens can be created with the Responsive Shared Page and Secure Fields

## 1.5.1

 - Updated to force TLS 1.2 to connect to Rapid

## 1.5.0

 - Updated dependencies: Newtonsoft.Json to v9.0.1 and Automapper to v4.1.1

## 1.4.0.0

 - Now thread safe (thanks @davidmiani)
 - Added ability to set the Rapid API version and new associated fields
 - Added `SecuredCardData` field and marked `ThirdPartyWalletID` as deprecated
 - Added wrapper `QueryTransaction` method which accepts an int instead of long

## 1.3.0.0

 - Added Settlement Search

## 1.2.1.0
 
 - Dependencies tightened to AutoMapper 3.3.1 - 4.1.1
 - Improved error handling for empty responses
 - Added convenience functions such as `hasFraudCode` and `getResponseMessages`
 - Changed create and update customer to use MOTO for TransactionType to support not sending the CVN.

## 1.2.0.0

 - Added ability to update a token customer using `UpdateCustomer`
 - Add `SaveCustomer` flag to Transactions to create a token when a transaction is completed.

## 1.1.3.0

 - Added Shipping and Invoice details to query transaction response
 
## 1.1.2.0

 - Added Shipping and Item details to transaction requests

## 1.1.1.0

 - Added SDK error codes to transalation resource for UserDisplayMessage
 - Exposed Responsive Shared Page variables for Transactions (such as CustomerReadOnly and LogoUrl)

## 1.1.0.0

 - Added Capture & Cancel functions for pre-auth payments
 - Added Email field for Customers
 - Marked Customer.CustomerIP as obselete, Transaction.CustomerIP should be used

## 1.0.1.0

 - Added error messages resource to NuGet package (no code changes)

## 1.0.0.0

 - First release
