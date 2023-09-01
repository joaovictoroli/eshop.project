﻿using AutoMapper;
using respapi.eshop.Models.DTOs;
using respapi.eshop.Models.Entities;

namespace respapi.eshop.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterDto, AppUser>();
            CreateMap<AppUser, MemberDto>();
            CreateMap<AddressDto, UserAdress>();
            CreateMap<UserAdress, AddressDto>();
        }
    }
}