using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.User;
using BookStoreApp.API.Static;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(ILogger<AuthController> logger, IMapper mapper, UserManager<ApiUser> userManager, IConfiguration configuration)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserRegisterDto userDto)
        {
            var user = _mapper.Map<ApiUser>(userDto);

            var results = await _userManager.CreateAsync(user, "password");

            if (!results.Succeeded)
            {
                _logger.LogError("Failed to create user: {0}", userDto.Email);

                foreach (var error in results.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            await _userManager.AddToRoleAsync(user, "User");

            return Accepted();
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginUserDto userDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userDto.Email);
                var validPassword = await _userManager.CheckPasswordAsync(user, userDto.Password);   

                if (user == null || validPassword == false)
                {
                    return Unauthorized();
                }

                string token = await GenerateToken(user);

                var response = new AuthResponse
                {
                    Token = token,
                    UserId = user.Id
                };

                return Accepted(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong here");
            }
            return Ok();
        }

        private async Task<string> GenerateToken(ApiUser user)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

            var userClaims = await _userManager.GetClaimsAsync(user);

            var clams = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.Uid, user.Id)
            }
            .Union(roleClaims)
            .Union(userClaims);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: clams,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:Duration"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
