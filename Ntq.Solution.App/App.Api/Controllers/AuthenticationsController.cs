using App.Api.Contracts.User.Request;
using App.Api.Filters;
using App.Api.Options;
using App.Domain.Entities;
using App.Domain.Interfaces.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    /// <summary>
    /// Information of AuthenticationsController
    /// CreatedBy: ThiepTT(28/02/2023)
    /// </summary>
    [Route(ApiRouter.ROUTER)]
    [ApiController]
    [AppExceptionAttibute]
    public class AuthenticationsController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Authentications Controller
        /// </summary>
        /// <param name="userService">userService</param>
        /// <param name="mapper">mapper</param>
        public AuthenticationsController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Login for email
        /// </summary>
        /// <param name="userEmailPassword">UserEmailPassword</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: ThiepTT(28/02/2023)
        [HttpPost]
        [Route(ApiRouter.Authentication.LOGINFOREMAIL)]
        public async Task<IActionResult> LoginForEmail([FromBody] UserEmailPassword userEmailPassword)
        {
            var user = _mapper.Map<User>(userEmailPassword);

            var result = await _userService.Login(user);

            return (result.IsError) ? HandlerErrorResponse(result.ErrorData) : Ok(result);
        }
    }
}