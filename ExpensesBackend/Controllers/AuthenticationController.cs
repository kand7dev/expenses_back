﻿using ExpensesCore;
using ExpensesCore.CustomExceptions;
using ExpensesDb;
using Microsoft.AspNetCore.Mvc;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace ExpensesBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(User user)
        {
            try {
                var result = await _userService.SignUp(user);
                return Created("", result);
            }
            catch (UsernameAlreadyExistsException ex)
            {
                return StatusCode(409, ex.Message);
            }
 
  
        }
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(User user)
        {
            try
            {
                var result = await _userService.SignIn(user);
                return Ok(result);
            }
            catch (InvalidUsernamePasswordException ex)
            {
                return StatusCode(401, ex.Message);
            }
        }
        [HttpPost("google")]
        public async Task<IActionResult> GoogleSignIn([FromQuery] string token)
        {
            var payload = await ValidateAsync(token, new ValidationSettings
            {
                Audience = new[] { Environment.GetEnvironmentVariable("CLIENT_ID")}
            });
            var result = await _userService.ExternalSignIn(new ExpensesDb.User
            {
                Email = payload.Email,
                ExternalId = payload.Subject,
                ExternalType = "GOOGLE"
            });
            return Created("", result);

        }
    }
}
