using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Mvc;
using respapi.eshop.Extensions;
using respapi.eshop.Interfaces;
using respapi.eshop.Models;
using respapi.eshop.Models.DTOs;
using respapi.eshop.Models.Entities;

namespace respapi.eshop.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly ICepService _cepService;
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;

        public UsersController(IMapper mapper, ICepService cepService,
                            IUserRepository userRepository, IAddressRepository addressRepository)
        {
            _mapper = mapper;
            _cepService = cepService;
            _userRepository = userRepository;
            _addressRepository = addressRepository;
        }


        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            if (username == User.GetUsername())
            {
                var appuser = await _userRepository.GetUserByUsernameAsync(username);
                return _mapper.Map<MemberDto>(appuser);

            }
            return BadRequest();
        }

        [HttpPost("register-adress/{cep}")]
        public async Task<ActionResult<AddressDto>> AddAdress(string cep, RegisterAdressDto registerAdress)
        {
            var apiresp = await _cepService.GetAdressByCep(cep);

            if (apiresp != null)
            {
                AppUser user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());                

                UserAdress userAdress = new UserAdress()
                {
                    Cep = apiresp.cep,
                    Uf = apiresp.uf,
                    Bairro = apiresp.bairro,
                    Complemento = apiresp.complemento,
                    Numero = registerAdress.Numero,
                    Apartamento = registerAdress.Apartamento,
                    InfoAdicinal = registerAdress.InfoAdicional,
                    AppUserId = User.GetUserId(),
                    IsMain = false
                };

                if (user.Adresses.Count == 0) { userAdress.IsMain = true; }

                await _addressRepository.AddUserAdress(userAdress);
                return _mapper.Map<AddressDto>(userAdress);
            }

            return NotFound("was not possible to finish operation");
            
        }

        [HttpDelete("delete-address/{addressId}")]
        public async Task<ActionResult> DeleteAddress(int addressId) 
        {
            var userAddress = await _addressRepository.GetUserAddressById(addressId);

            if (userAddress == null) return NotFound();

            if (userAddress.IsMain) return BadRequest("You cannot delete your main address");

            var isDeleted = await _addressRepository.DeleteUserAddress(userAddress);

            if (isDeleted == false) { return BadRequest("Something went wrong"); }

            return Ok("Addres got deleted successfully");  
        }

        [HttpPost("set-main-address/{addressId}")]
        public async Task<ActionResult> SetMainAddress(int addressId)
        {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

            var userAddress = user.Adresses.FirstOrDefault(x=> x.Id == addressId);

            if (userAddress == null) { return BadRequest("Address not found"); }

            if (userAddress.IsMain) { return BadRequest("This is already your main address"); }

            var currentMain = user.Adresses.FirstOrDefault(x => x.IsMain);

            if (currentMain == null) { return BadRequest("Main Address not found"); }

            var isDone = _addressRepository.ChangeMainAddress(currentMain, userAddress);

            return Ok("Main address changed successfully");
        }

    }
}
