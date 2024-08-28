using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using System.Reflection.Metadata.Ecma335;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        //post method

        [HttpPost]
        [Route("Register")]
                                                            //pass the dto to the controller
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.UserName
            };

           var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if(identityResult.Succeeded)
            {
                //Add role to the user
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    foreach (var role in registerRequestDto.Roles)
                    {
                        identityResult = await userManager.AddToRoleAsync(identityUser, role);

                        if (!identityResult.Succeeded)
                        {
                            // Handle the error if adding a role fails, you can collect all errors and return them
                            return BadRequest(identityResult.Errors);
                        }
                    }
                    return Ok("User was registered and roles were added successfully");
                }
                else
                {
                    return Ok("User was registered successfully without roles");
                }

            }
            return BadRequest("Cannot register the user");

        }
        

    }
}
