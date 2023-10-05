﻿using AutoMapper;
using respapi.eshop.Models.DTOs;
using respapi.eshop.Models.DTOs.OrderDtos;
using respapi.eshop.Models.Entities;

namespace respapi.eshop.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterDto, AppUser>();
            CreateMap<AppUser, UserDetailsDto>();
            CreateMap<AddressDto, UserAddress>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<SubCategoryDto, SubCategory>().ReverseMap();
            CreateMap<AddCategoryDto, Category>().ReverseMap();
            CreateMap<CategoryDto, Category>().ReverseMap();

            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<UserAddress, OrderAddress>().ReverseMap();
            CreateMap<OrderProduct, OrderProductDto>().ReverseMap();
             CreateMap<OrderAddress, OrderAddressDto>().ReverseMap();
        }
    }
}