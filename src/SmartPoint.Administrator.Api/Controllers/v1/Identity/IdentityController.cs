using Asp.Versioning;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartPoint.Administrator.Api.Extensions;
using SmartPoint.Administrator.Api.Shared;
using SmartPoint.Administrator.ApplicationService.Administrator.Response;
using SmartPoint.Administrator.ApplicationService.Identity.Request;
using SmartPoint.Administrator.ApplicationService.Shared.Interfaces;
using SmartPoint.Administrator.Infra.Identity.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace SmartPoint.Administrator.Api.Controllers.v1.Identity
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/identity")]
    public class IdentityController : MainController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppSettings _appSettings;

        public IdentityController(
            INotificator notificator,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<AppSettings> appSettings
        ) : base(notificator)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        [Route("get-users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();

            return CustomResponse(HttpStatusCode.OK, users);
        }

        [HttpPost]
        [Route("register-new-user")]
        public async Task<IActionResult> Register(UserRegisterRequest userRegister)
        {
            if (!ModelState.IsValid)
            {
                NotifyError(ModelState.Values);

                return CustomResponse();
            }

            var user = new ApplicationUser
            {
                FullName = userRegister.FullName,
                UserName = userRegister.Email,
                Email = userRegister.Email,
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(user, userRegister.Password);

            if (!result.Succeeded)
            {
                if (result.Errors.Any())
                {
                    NotifyError(result.Errors);

                    return CustomResponse();
                }

                return CustomResponse(HttpStatusCode.BadRequest, "Ocorreu um erro no cadastro do usuário.");
            }

            return CustomResponse(HttpStatusCode.OK, $"Usuário '{userRegister.FullName}' adicionado com sucesso!");
        }

        [HttpPost]
        [Route("register-role")]
        public async Task<IActionResult> RegisterRole(RoleRegisterRequest roleRequest)
        {
            if (!ModelState.IsValid)
            {
                NotifyError(ModelState.Values);

                return CustomResponse();
            }

            var roleExists = await _roleManager.RoleExistsAsync(roleRequest.Name);

            if (roleExists) return CustomResponse(HttpStatusCode.Conflict, $"A role '{roleRequest.Name}' já existe.");

            var result = await _roleManager.CreateAsync(new IdentityRole(roleRequest.Name));

            if (!result.Succeeded)
            {
                if (result.Errors.Any())
                {
                    NotifyError(result.Errors);

                    return CustomResponse();
                }

                return CustomResponse(HttpStatusCode.BadRequest, "Ocorreu um erro no cadastro da role.");
            }

            return CustomResponse(HttpStatusCode.OK, $"Role '{roleRequest.Name}' adicionada com sucesso!");
        }

        [HttpPost]
        [Route("add-claim-to-user")]
        public async Task<IActionResult> AddClaimToUser(AddClaimUserRequest claimUserRequest)
        {
            if (!ModelState.IsValid)
            {
                NotifyError(ModelState.Values);

                return CustomResponse();
            }

            var user = await _userManager.FindByIdAsync(claimUserRequest.UserId.ToString());

            if (user == null) return CustomResponse(HttpStatusCode.NotFound, $"Usuário não encontrado.");

            var result = await _userManager.AddClaimAsync(user, new Claim(claimUserRequest.ClaimType, claimUserRequest.ClaimValue));

            if (!result.Succeeded)
            {
                if (result.Errors.Any())
                {
                    NotifyError(result.Errors);

                    return CustomResponse();
                }

                return CustomResponse(HttpStatusCode.BadRequest, "Ocorreu um erro no cadastro da claim para o usuário.");
            }

            return CustomResponse(HttpStatusCode.OK, $"Claim '{claimUserRequest.ClaimType}={claimUserRequest.ClaimValue}' adicionada ao usuário '{user.FullName}' com sucesso!");
        }

        [HttpPost]
        [Route("add-claim-to-role")]
        public async Task<IActionResult> AddClaimToRole(AddClaimRoleRequest claimRoleRequest)
        {
            if (!ModelState.IsValid)
            {
                NotifyError(ModelState.Values);

                return CustomResponse();
            }

            var role = await _roleManager.FindByIdAsync(claimRoleRequest.RoleId.ToString());

            if (role == null) return CustomResponse(HttpStatusCode.NotFound, $"Role {claimRoleRequest.RoleName} não encontrada.");

            var result = await _roleManager.AddClaimAsync(role, new Claim(claimRoleRequest.ClaimType, claimRoleRequest.ClaimValue));

            if (!result.Succeeded)
            {
                if (result.Errors.Any())
                {
                    NotifyError(result.Errors);

                    return CustomResponse();
                }

                return CustomResponse(HttpStatusCode.BadRequest, "Ocorreu um erro no cadastro da claim para a role.");
            }

            return CustomResponse(HttpStatusCode.OK, $"Claim '{claimRoleRequest.ClaimType}={claimRoleRequest.ClaimValue}' adicionada à role '{claimRoleRequest.RoleName}' com sucesso!");
        }

        [HttpPost]
        [Route("associate-user-role")]
        public async Task<IActionResult> AssociateUserRole(AssociateUserRole associateUserRole)
        {
            if (!ModelState.IsValid)
            {
                NotifyError(ModelState.Values);

                return CustomResponse();
            }

            var user = await _userManager.FindByIdAsync(associateUserRole.UserId.ToString());

            if (user == null) return CustomResponse(HttpStatusCode.NotFound, $"Usuário '{associateUserRole.Username}' não encontrado.");

            var roleExists = await _roleManager.RoleExistsAsync(associateUserRole.RoleName);

            if (!roleExists) return CustomResponse(HttpStatusCode.NotFound, $"Role '{associateUserRole.RoleName}' não encontrada.");

            var isInRole = await _userManager.IsInRoleAsync(user, associateUserRole.RoleName);

            if (isInRole) return CustomResponse(HttpStatusCode.Conflict, $"Usuário '{associateUserRole.Username}' já está na role '{associateUserRole.RoleName}'.");

            var result = await _userManager.AddToRoleAsync(user, associateUserRole.RoleName);

            if (!result.Succeeded)
            {
                if (result.Errors.Any())
                {
                    NotifyError(result.Errors);

                    return CustomResponse();
                }

                return CustomResponse(HttpStatusCode.BadRequest, "Ocorreu um erro no cadastro da role para o usuário.");
            }

            return CustomResponse(HttpStatusCode.OK, $"Associação do usuário {associateUserRole.Username} com a role {associateUserRole.RoleName} realizada com sucesso!");
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginRequest userLogin)
        {
            if (!ModelState.IsValid)
            {
                NotifyError(ModelState.Values);

                return CustomResponse();
            }

            var result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, true);

            if (!result.Succeeded)
            {
                if (result.IsLockedOut) return CustomResponse(HttpStatusCode.BadRequest, "Usuário bloqueado temporariamente.");

                if (result.IsNotAllowed) return CustomResponse(HttpStatusCode.BadRequest, "Usuário sem permissão.");

                if (result.RequiresTwoFactor) return CustomResponse(HttpStatusCode.BadRequest, "Usuário requer validação duplo fator.");

                return CustomResponse(HttpStatusCode.BadRequest, "Ocorreu um erro na autenticação. Tente novamente mais tarde.");
            }

            return CustomResponse(HttpStatusCode.OK, await GerarJwt(userLogin.Email));
        }

        private async Task<UserResponseLogin> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user!);
            var userRoles = await _userManager.GetRolesAsync(user!);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user!.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email!));
            claims.Add(new Claim(JwtRegisteredClaimNames.Name, user.FullName!));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.UtcNow.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));

                var role = await _roleManager.FindByNameAsync(userRole);

                if (role == null) continue;

                var roleClaims = await _roleManager.GetClaimsAsync(role);

                foreach (var roleClaim in roleClaims)
                    claims.Add(roleClaim);
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
                    FullName = user.FullName,
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
