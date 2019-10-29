using System;
using System.Linq;
using eWAY.Rapid.Internals.Enums;
using eWAY.Rapid.Internals.Mappings;
using eWAY.Rapid.Internals.Models;
using eWAY.Rapid.Internals.Request;
using eWAY.Rapid.Internals.Response;
using eWAY.Rapid.Models;
using Mapster;
using BaseResponse = eWAY.Rapid.Internals.Response.BaseResponse;
using CardDetails = eWAY.Rapid.Models.CardDetails;
using Customer = eWAY.Rapid.Models.Customer;
using LineItem = eWAY.Rapid.Models.LineItem;
using Option = eWAY.Rapid.Internals.Models.Option;
using Payment = eWAY.Rapid.Internals.Models.Payment;
using Refund = eWAY.Rapid.Models.Refund;
using ShippingAddress = eWAY.Rapid.Models.ShippingAddress;
using VerificationResult = eWAY.Rapid.Models.VerificationResult;

namespace eWAY.Rapid.Internals.Services
{
    internal class MappingService: IMappingService
    {
        private static IAdapter config { get; set; }
        public static void InitializeMappingService()
        {
            TypeAdapterConfig.GlobalSettings.Default.IgnoreNullValues(true);
            config = new Adapter(TypeAdapterConfig.GlobalSettings);
            
            CustomMapProfile.CreateCustomMapProfile(config);
            EntitiesMapProfile.CreateEntitiesMapProfile(config);
            RequestMapProfile.CreateRequestMapProfile(config);
            ResponseMapProfile.CreateResponseMapProfile(config);
        }

        public MappingService()
        {
            InitializeMappingService();
        }

        public TDest Map<TSource, TDest>(TSource obj)
        {
            return config.Adapt<TSource, TDest>(obj);
        }

     
    }
}
