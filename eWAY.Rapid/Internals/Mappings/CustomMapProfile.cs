using System;
using AutoMapper;
using eWAY.Rapid.Models;
using Option = eWAY.Rapid.Internals.Models.Option;

namespace eWAY.Rapid.Internals.Mappings {
    internal class CustomMapProfile : Profile {
        public CustomMapProfile() {
            CreateMap<String, Option>(MemberList.None).ConvertUsing(s => new Option { Value = s });
            CreateMap<Option, String>(MemberList.None).ConvertUsing(o => o.Value);
            CreateMap<bool?, TransactionStatus>(MemberList.None).AfterMap((b, t) => t.Status = b);
        }
    }
}
