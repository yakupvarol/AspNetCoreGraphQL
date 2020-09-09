using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreGraphQL.Business;
using AspNetCoreGraphQL.DTO.User;
using AspNetCoreGraphQL.GraphQL;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspNetCoreGraphQL.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _log;

        public UserController(IUserService userService, ILogger<UserController> log)
        {
            _userService = userService;
            _log = log;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] UserListRequestDTO src)
        {
            return Ok(_userService.List(src));
        }

    }
}