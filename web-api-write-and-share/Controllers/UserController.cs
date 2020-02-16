using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using web_api_write_and_share.Contracts;
using web_api_write_and_share.Controllers.Requests;
using web_api_write_and_share.Controllers.Response;
using web_api_write_and_share.Entities;

namespace web_api_write_and_share.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IIdentityService identityService;

        public UserController(IIdentityService _identityService)
        {
            identityService = _identityService;
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Identity.Register)]

        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            string erro;

            if (!ValidatePassword(request.Password, out erro))
            {
                return BadRequest(erro.ToString());
            }

            if (!IsValidEmail(request.Email) || request.Email == string.Empty)
            {
                return BadRequest(new FailedResponse
                {
                    Errors = new[] { "Introduza um Email Válido" }
                });
            }

            var authResponse = await identityService.RegisterAsync(request);

            if (!authResponse.Success)
            {
                return BadRequest(new FailedResponse
                {
                    Errors = authResponse.Errors
                });
            }
            return Ok(new SucessResponse
            {
                Token = authResponse.Token
            });

        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var authResponse = await identityService.LoginAsync(request);

            if (!authResponse.Success)
            {
                return BadRequest(new FailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new SucessResponse
            {
                Token = authResponse.Token

            });
        }

        [HttpGet(ApiRoutes.Identity.GetUserById)]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            var user = await identityService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }


            return Ok(user);
        }

        [HttpGet(ApiRoutes.Identity.GetAllUsers)]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await identityService.GetUsersAsync());

        }

        [HttpDelete(ApiRoutes.Identity.DeleteUser)]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            var removed = await identityService.DeleteUserAsync(userId);

            if (!removed)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut(ApiRoutes.Identity.UpdateUser)]
        public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UserUpdateRequest request)
        {
            string erro;

            if (!ValidatePassword(request.Password, out erro))
            {
                return BadRequest(erro.ToString());
            }

            if (!(request.Password == request.ComfirmPassword))
            {
                return BadRequest(
                    new FailedResponse
                    {
                        Errors = new[] { "As passwords não coincidem!" }
                    });
            }

            if (!IsValidEmail(request.Email) || request.Email == string.Empty)
            {
                return BadRequest(new FailedResponse
                {
                    Errors = new[] { "Introduza um Email Válido" }
                });
            }

            var updated = await identityService.UpdateUserAsync(userId, request);
            if (updated)
            {
                return Ok(request);
            }
            return NotFound();
        }

        private bool ValidatePassword(string password, out string ErrorMessage)
        {
            var input = password;
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Password should not be empty");
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasMiniMaxChars = new Regex(@".{8,15}");
            var hasLowerChar = new Regex(@"[a-z]+");


            if (!hasLowerChar.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one lower case letter";
                return false;
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                ErrorMessage = "Password should not be less than 8 or greater than 15 characters";
                return false;
            }
            else if (!hasNumber.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one numeric value";
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsValidEmail(string source)
        {
            return new EmailAddressAttribute().IsValid(source);
        }


    }

}
