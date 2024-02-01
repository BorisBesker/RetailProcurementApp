using AutoMapper;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailProcurementApp.Dto;
using ServiceLayer.Services;

namespace RetailProcurementApp.Controllers
{

    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(TokenDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Login(UserLoginDto userLoginModel)
        {
            try
            {
                if (userLoginModel == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Not a valid model");
                }

                var userModel = _mapper.Map<User>(userLoginModel);

                var token = _userService.Login(userModel);
                if (token == null || token == string.Empty)
                {
                    ModelState.AddModelError("", "UserName or Password is incorrect");
                    return StatusCode(422, ModelState);
                }

                return Ok(new TokenDto { Token = token});
            }
            catch (Exception)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult Register(UserRegisterDto user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Not a valid model");
                }

                var userDbModel = _mapper.Map<User>(user);

                var response = _userService.Reigster(userDbModel);

                if (response.UserNameExists)
                {
                    ModelState.AddModelError("", "Username must be unqiue");
                    return StatusCode(422, ModelState);
                }

                return Ok();
            }
            catch (Exception)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
