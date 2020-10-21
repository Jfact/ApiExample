using System.Threading.Tasks;
using ApiApplication.Controllers.Filters;
using AutoMapper;
using EntitiesLibrary.Configuration;
using EntitiesLibrary.Entities;
using EntitiesLibrary.Entities.DataTransfer;
using IdentityLibrary;
using LoggerLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IIdentityManager _identityManager;
        private async Task<User> ApplicationUser() =>
            await _userManager.FindByEmailAsync(User.Identity.Name);

        public UserController(
            ILoggerManager logger,
            IMapper mapper,
            IIdentityManager identityManager,
            UserManager<User> userManager)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _identityManager = identityManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            return Ok(
                _mapper.Map<UserDto>(await _userManager.FindByEmailAsync(User.Identity.Name))
                );
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationActionFilter))]
        public async Task<IActionResult> Post([FromBody] UserCreateDto model)
        {
            User user = _mapper.Map<User>(model);
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            await _userManager.AddToRolesAsync(user, new[] { IdentityRoles.ClientRoleName });

            return StatusCode(201);
        }

        [HttpPut]
        [Authorize]
        [ServiceFilter(typeof(ValidationActionFilter))]
        public async Task<IActionResult> Put([FromBody] UserEditDto model)
        {
            User user = await _userManager.FindByEmailAsync(User.Identity.Name);

            if (user == null)
            {
                _logger.LogWarn($"{nameof(Post)}: Edit failed. User is not authorized.");

                return Unauthorized();
            }

            if (!await _identityManager.ValidateUser(user.Email, model.PasswordForValidation))
            {
                _logger.LogWarn($"{nameof(Post)}: Edit failed. Email and password do not match.");

                ModelState.TryAddModelError(nameof(model.PasswordForValidation), "Incorect password for validation");

                return BadRequest(ModelState);
            }

            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                await _userManager.ChangePasswordAsync(user, model.PasswordForValidation, model.Password);
            }

            IdentityResult result = await _userManager.UpdateAsync(_mapper.Map(model, user));

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpPost("[action]")]
        [ServiceFilter(typeof(ValidationActionFilter))]
        public async Task<IActionResult> Login([FromBody] UserLoginDto model)
        {
            if (!await _identityManager.ValidateUser(model.Email, model.Password))
            {
                _logger.LogWarn($"{nameof(Post)}: Login failed. Wrong user name or password.");

                return Unauthorized();
            }

            return Ok(
                new { Token = await _identityManager.CreateToken() }
                );
        }
    }
}
