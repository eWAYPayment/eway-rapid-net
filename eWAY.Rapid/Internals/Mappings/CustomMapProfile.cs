using System;
using Mapster;
using eWAY.Rapid.Models;
using Option = eWAY.Rapid.Internals.Models.Option;

namespace eWAY.Rapid.Internals.Mappings {
    internal class CustomMapProfile {
        public static void CreateCustomMapProfile(IAdapter adapter)
        {
            adapter.BuildAdapter(TypeAdapterConfig<String, Option>.NewConfig()
                .Map(dest => dest.Value, src => src).TwoWays());
            adapter.BuildAdapter(TypeAdapterConfig<bool?, TransactionStatus>.NewConfig()
                .AfterMapping((b, t) => t.Status = b));
        }
    }
}