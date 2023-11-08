using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Models.DTO;
using TodoList.Models.Entities;
using TodoList.Services;

namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        private readonly IMapper _mapper;

        public AuthenticationController(IAuthenticationService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Register(UserDTO request)
        {
            var newUser = _mapper.Map<ApplicationUser>(request);
            var user = await _authenticationService.Register(newUser);
            if (user == null)
            {
                return BadRequest("User with that name already exists");
            }
            return Created($"~api/users/{user.Id}", user);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Login(UserDTO request)
        {
            var newUser = _mapper.Map<ApplicationUser>(request); 
            var user = await _authenticationService.Login(newUser);
            if(user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(_mapper.Map<UserDTO>(user));
        }
    }
}
