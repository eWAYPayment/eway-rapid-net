# eWAY Rapid .NET Library

[![Latest version on NuGet][ico-version]][link-nuget]
[![Software License][ico-license]](LICENSE.md)
[![Build Status][ico-appveyor]][link-appveyor]

A .NET library to integrate with eWAY's Rapid Payment API.

Sign up with eWAY at:
 - Australia:    https://www.eway.com.au/
 - New Zealand:  https://eway.io/nz/
 - Hong Kong:    https://eway.io/hk/
 - Malaysia:     https://eway.io/my/
 - Singapore:    https://eway.io/sg/

For testing, get a free eWAY Partner account: https://www.eway.com.au/developers

## Install

### Install with NuGet

The eWAY Rapid .NET library can be easily added to your project with [NuGet](https://www.nuget.org/packages/eWAY.Rapid/).
Versions 4.5 or above of .NET are supported at this time.

 1. In Visual Studio, open the NuGet Package Manager
 2. Using the Search box, search for "eWAY"
 3. Click "Install" and select the projects you'd like the eWAY package to be available for
 4. NuGet will download the eWAY library & dependencies
 5. You are set to use eWAY in your project!
 
## Usage

See the [eWAY Rapid API Reference](https://eway.io/api-v3/) for usage details.

A simple Direct payment example:

```csharp
using eWAY.Rapid;
using eWAY.Rapid.Enums;
using eWAY.Rapid.Models;

string APIKEY = "Rapid API Key";
string PASSWORD = "Rapid API Password";
string ENDPOINT = "Sandbox";

IRapidClient ewayClient = RapidClientFactory.NewRapidClient(APIKEY, PASSWORD, ENDPOINT);

Transaction transaction = new Transaction(){
    Customer = new Customer() { 
        CardDetails = new CardDetails()
        {
            Name = "John Smith",
            Number = "4444333322221111",
            ExpiryMonth = "11",
            ExpiryYear = "22",
            CVN = "123"
        } 
    },
    PaymentDetails = new PaymentDetails()
    {
        TotalAmount = 1000
    },
    TransactionType = TransactionTypes.Purchase
};

CreateTransactionResponse response = ewayClient.Create(PaymentMethod.Direct, transaction);

if (response.TransactionStatus != null && response.TransactionStatus.Status == true)
{
    int transactionID = response.TransactionStatus.TransactionID;
}
```

## Change log

Please see [CHANGELOG](CHANGELOG.md) for more information what has changed recently.

## License

The MIT License (MIT). Please see [License File](LICENSE.md) for more information.

[ico-version]: https://img.shields.io/nuget/v/eWAY.Rapid.svg?style=flat-square
[ico-license]: https://img.shields.io/badge/license-MIT-brightgreen.svg?style=flat-square
[ico-appveyor]: https://img.shields.io/appveyor/ci/eWAY/eway-rapid-net/master.svg?style=flat-square

[link-nuget]: https://www.nuget.org/packages/eWAY.Rapid/
[link-appveyor]: https://ci.appveyor.com/project/eWAY/eway-rapid-net