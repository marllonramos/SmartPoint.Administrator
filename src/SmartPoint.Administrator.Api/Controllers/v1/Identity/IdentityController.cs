using Asp.Versioning;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartPoint.Administrator.Api.Extensions;
using SmartPoint.Administrator.ApplicationService.Administrator.Response;
using SmartPoint.Administrator.ApplicationService.Identity.Request;
using SmartPoint.Administrator.Infra.Identity.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmartPoint.Administrator.Api.Controllers.v1.Identity
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/identity")]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppSettings _appSettings;

        public IdentityController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        [Route("get-users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost]
        [Route("register-new-user")]
        public async Task<IActionResult> Register(UserRegisterRequest userRegister)
        {
            if (!ModelState.IsValid) return BadRequest();

            var user = new ApplicationUser
            {
                UserName = userRegister.Email,
                Email = userRegister.Email,
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(user, userRegister.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);

                return Ok();
            }

            if (result.Errors.Any())
                return BadRequest(result.Errors);

            return BadRequest();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginRequest userLogin)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, true);

            if (result.Succeeded) return Ok(await GerarJwt(userLogin.Email));

            return BadRequest();
        }

        private async Task<UserResponseLogin> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user!);
            var userRoles = await _userManager.GetRolesAsync(user!);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user!.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email!));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.UtcNow.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings!.Secret!);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidateIn,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new UserResponseLogin
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationHours).TotalSeconds,
                UserToken = new UserToken
                {
                    Id = user.Id,
                    Email = user.Email!,
                    Claims = claims.Select(c => new UserClaim
                    {
                        Type = c.Type,
                        Value = c.Value,
                    })
                }
            };

            return response;
        }
    }
}
