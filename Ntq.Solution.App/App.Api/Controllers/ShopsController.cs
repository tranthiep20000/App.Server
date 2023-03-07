using App.Api.Contracts.Shop.Requests;
using App.Api.Filters;
using App.Api.Options;
using App.Domain.Entities;
using App.Domain.Interfaces.IRepositories;
using App.Domain.Interfaces.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace App.Api.Controllers
{
    /// <summary>
    /// Information of ShopsController
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    [Route(ApiRouter.ROUTER)]
    [ApiController]
    [AppExceptionAttibute]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ShopsController : BaseController
    {
        private readonly IShopRepository _shopRepository;
        private readonly IShopService _shopService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Shops Controller
        /// </summary>
        /// <param name="shopRepository">shopRepository</param>
        /// <param name="shopService">shopService</param>
        /// <param name="mapper">mapper</param>
        public ShopsController(IShopRepository shopRepository, IShopService shopService, IMapper mapper) 
        {
            _shopRepository = shopRepository;
            _shopService = shopService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all shop
        /// </summary>
        /// <returns>IActionResult</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        [HttpGet]
        public async Task<IActionResult> GetAllShop()
        {
            var result = await _shopRepository.GetAll();

            return (result.IsError) ? HandlerErrorResponse(result.ErrorData) : Ok(result);
        }

        /// <summary>
        /// Get shop by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        [HttpGet]
        [Route(ApiRouter.ID)]
        public async Task<IActionResult> GetShopById([Required] int id)
        {
            var result = await _shopRepository.GetById(id);

            return (result.IsError) ? HandlerErrorResponse(result.ErrorData) : Ok(result);
        }

        /// <summary>
        /// Create shop
        /// </summary>
        /// <param name="shopCreate">ShopCreate</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        [HttpPost]
        public async Task<IActionResult> CreateShop([FromBody] ShopCreate shopCreate)
        {
            var shop = _mapper.Map<Shop>(shopCreate);

            var result = await _shopService.Create(shop);

            return (result.IsError) ? HandlerErrorResponse(result.ErrorData) : Ok(result);
        }

        /// <summary>
        /// Update shop
        /// </summary>
        /// <param name="shopUpdate">ShopUpdate</param>
        /// <param name="id">id</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        [HttpPut]
        [Route(ApiRouter.ID)]
        public async Task<IActionResult> UpdateUser([FromBody] ShopUpdate shopUpdate, [Required] int id)
        {
            var shop = _mapper.Map<Shop>(shopUpdate);

            var result = await _shopService.Update(shop, id);

            return (result.IsError) ? HandlerErrorResponse(result.ErrorData) : Ok(result);
        }

        /// <summary>
        /// Delete shop
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        [HttpDelete]
        [Route(ApiRouter.ID)]
        public async Task<IActionResult> DeleteShop([Required] int id)
        {
            var result = await _shopService.Delete(id);

            return (result.IsError) ? HandlerErrorResponse(result.ErrorData) : Ok(result);
        }
    }
}