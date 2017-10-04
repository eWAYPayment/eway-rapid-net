using System;
using System.Linq;
using AutoMapper;
using eWAY.Rapid.Internals.Enums;
using eWAY.Rapid.Internals.Mappings;
using eWAY.Rapid.Internals.Models;
using eWAY.Rapid.Internals.Request;
using eWAY.Rapid.Internals.Response;
using eWAY.Rapid.Models;
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
        private static readonly MapperConfiguration config;
        static MappingService()
        {
            config = new MapperConfiguration(c => {
                c.AddProfile<CustomMapProfile>();
                c.AddProfile<EntitiesMapProfile>();
                c.AddProfile<RequestMapProfile>();
                c.AddProfile<ResponseMapProfile>();
            });
        }

        public MappingService()
        {
        }

        public TDest Map<TSource, TDest>(TSource obj)
        {
            return config.CreateMapper().Map<TSource, TDest>(obj);
        }

     
    }
}
