using App.Api.Contracts.User.Requests;
using App.Api.Filters;
using App.Api.Options;
using App.Domain.Entities;
using App.Domain.Enum;
using App.Domain.Interfaces.IRepositories;
using App.Domain.Interfaces.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace App.Api.Controllers
{
    /// <summary>
    /// Information of UsersController
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    [Route(ApiRouter.ROUTER)]
    [ApiController]
    [AppExceptionAttibute]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : BaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Users Controller
        /// </summary>
        /// <param name="userRepository">userRepository</param>
        /// <param name="userService">userService</param>
        /// <param name="mapper">mapper</param>
        public UsersController(IUserRepository userRepository, IUserService userService, IMapper mapper)
        {
            _userRepository = userRepository;
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all user
        /// </summary>
        /// <returns>IActionResult</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var result = await _userRepository.GetAll();

            return (result.IsError) ? HandlerErrorResponse(result.ErrorData) : Ok(result);
        }

        /// <summary>
        /// Get all paging user
        /// </summary>
        /// <param name="valueFilter">ValueFilter</param>
        /// <param name="createAt">CreateAt</param>
        /// <param name="typeEnum">TypeEnum</param>
        /// <param name="deleteEnum">DeleteEnum</param>
        /// <param name="pageNumber">PageNumber</param>
        /// <param name="pageSize">PageSize</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        [HttpGet]
        [Route(ApiRouter.User.GETALLPAGINGUSER)]
        public async Task<IActionResult> GetAllPagingUser(string? valueFilter, DateTime? createAt, TypeEnum? typeEnum,
            DeleteEnum? deleteEnum, int pageNumber, int pageSize = 10)
        {
            var result = await _userRepository.GetAllPaging(valueFilter, createAt, typeEnum, deleteEnum, pageNumber, pageSize);

            return (result.IsError) ? HandlerErrorResponse(result.ErrorData) : Ok(result);
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        [HttpGet]
        [Route(ApiRouter.ID)]
        public async Task<IActionResult> GetUserById([Required] int id)
        {
            var result = await _userRepository.GetById(id);

            return (result.IsError) ? HandlerErrorResponse(result.ErrorData) : Ok(result);
        }

        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="userCreate">UserCreate</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreate userCreate)
        {
            var user = _mapper.Map<User>(userCreate);

            var result = await _userService.Create(user);

            return (result.IsError) ? HandlerErrorResponse(result.ErrorData) : Ok(result);
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="userUpdate">UpdateUser</param>
        /// <param name="id">id</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        [HttpPut]
        [Route(ApiRouter.ID)]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdate userUpdate, [Required] int id)
        {
            var user = _mapper.Map<User>(userUpdate);

            var result = await _userService.Update(user, id);

            return (result.IsError) ? HandlerErrorResponse(result.ErrorData) : Ok(result);
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        [HttpDelete]
        [Route(ApiRouter.ID)]
        public async Task<IActionResult> DeleteUser([Required] int id)
        {
            var result = await _userService.Delete(id);

            return (result.IsError) ? HandlerErrorResponse(result.ErrorData) : Ok(result);
        }
    }
}