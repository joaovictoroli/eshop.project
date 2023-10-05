using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using respapi.eshop.Extensions;
using respapi.eshop.Interfaces;
using respapi.eshop.Models.DTOs;
using respapi.eshop.Models.Entities;

namespace respapi.eshop.Controllers;

[Authorize]
public class OrderController : BaseApiController
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;    
    private readonly IMapper _mapper;

    public OrderController(IOrderRepository orderRepository ,IMapper mapper, IUserRepository userRepository, IProductRepository productRepository)
    {
        _userRepository = userRepository;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }  
    
    [HttpPost]
    public async Task<ActionResult<OrderDto>> CreateOrder(AddOrderDto addOrder)
    {
        if (addOrder.OrderProducts is null) { return BadRequest("Products are required");}

        var username = User.GetUsername();
        var appuser = await _userRepository.GetUserByUsernameAsync(username);
        var mainAddress = appuser.Addresses?.Where(x => x.IsMain == true).FirstOrDefault();

        if (mainAddress is null) { return BadRequest("Main address not found"); }        

        float? totalPrice = null;
        List<OrderProduct> orderProducts = new List<OrderProduct>();

        var result = await CalculateOrderDetails(addOrder);

        if (result.HasValue)
        {
            totalPrice = result.Value.totalPrice;
            orderProducts = result.Value.orderProducts;
        } else { return BadRequest("Something went wrong with the products"); }

        var order = new Order() 
        {
            OrderAddress =  _mapper.Map<OrderAddress>(mainAddress),
            OrderAddressId = mainAddress.Id,
            AppUser = appuser,
            UserId = appuser.Id,
            TotalPrice = (decimal)totalPrice!,
            SubmittedAt = DateTime.Now,
            Products = orderProducts
        };

        await _orderRepository.CreateOrder(order);
        return _mapper.Map<OrderDto>(order);
    }


    private async Task<(float? totalPrice, List<OrderProduct> orderProducts)?> CalculateOrderDetails(AddOrderDto addOrderDto) 
    {
        float? totalPrice = 0;
        List<OrderProduct> orderProducts = new List<OrderProduct>();
        foreach (var productOrder in addOrderDto.OrderProducts!) 
        {            
            var product = await _productRepository.GetProductByName(productOrder.ProductName!);
            if (product != null)
            {
                totalPrice += product.Price * productOrder.Quantity;
                var orderProduct = new OrderProduct
                {
                    ProductId = product.Id,
                    Quantity = productOrder.Quantity,
                    Price = (decimal)product.Price!,
                    ProductName = product.Name!,
                    ProductImageUrl = product.ImageUrl!
                };

                orderProducts.Add(orderProduct);
            } else {
                return null;
            }
        }
        return (totalPrice, orderProducts);
    }


}