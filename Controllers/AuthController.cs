using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
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

        //post method for logging in
        [HttpPost]
        [Route("Ligin")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.UserName);

            if (user != null)
            {
                var paswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (paswordResult)
                {
                    //get role of the user
                    var roles = await userManager.GetRolesAsync(user);
                    if(roles != null && roles.Any())
                    {
                        //create and assign a token
                        var jwtToken = tokenRepository.createJWTToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken,
                        };
                        
                        return Ok(jwtToken);
                    }

                    

                    return Ok("Successful");
                }
            }
            
            
                return BadRequest("Wrong username or password");
            


        }
        

    }
}
