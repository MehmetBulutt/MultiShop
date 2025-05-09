using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers;
using MultiShop.Order.Application.Features.CQRS.Queries.AddressQueries;
using MultiShop.Order.Application.Features.CQRS.Results.AddressResults;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly GetAddressQueryHandler _getaddressQueryHandler;
        private readonly GetAddressByIdQueryHandler _getaddressByIdQueryHandler;
        private readonly CreateAddressCommandHandler _createaddressCommandHandler;
        private readonly UpdateAddressCommandHandler _updateaddressCommandHandler;
        private readonly RemoveAddressCommandHandler _removeaddressCommandHandler;
        public AddressesController(GetAddressQueryHandler getaddressQueryHandler, GetAddressByIdQueryHandler getaddressByIdQueryHandler, CreateAddressCommandHandler createaddressCommandHandler, UpdateAddressCommandHandler updateaddressCommandHandler, RemoveAddressCommandHandler removeaddressCommandHandler)
        {
            _getaddressQueryHandler = getaddressQueryHandler;
            _getaddressByIdQueryHandler = getaddressByIdQueryHandler;
            _createaddressCommandHandler = createaddressCommandHandler;
            _updateaddressCommandHandler = updateaddressCommandHandler;
            _removeaddressCommandHandler = removeaddressCommandHandler;
        }

        [HttpGet]
        public async Task<IActionResult> AddressList()
        {
            var values =await _getaddressQueryHandler.Handle();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddressById(int id)
        {
            var values = await _getaddressByIdQueryHandler.Handle(new GetAddressByIdQuery(AddressId));
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress(CreateAddressCommand command)
        {
            await _createaddressCommandHandler.Handle(command);
            return Ok("Adres bilgisi başarıyla eklendi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAddress(UpdateAddressCommand command)
        {
            await _updateaddressCommandHandler.Handle(command);
            return Ok("Adres bilgisi başarıyla güncellendi");
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveAddress(int id)
        {
            await _removeaddressCommandHandler.Handle(new RemoveAddressCommand(id));
            return Ok("Adres  başarıyla silindi");
        }
    }
}
