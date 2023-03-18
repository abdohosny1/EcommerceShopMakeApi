using EcommerceShop.Api.Extensision;
using EcommerceShop.Core.Model.identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcountController : ControllerBase
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AcountController(
            UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, 
            ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            //add
            // var email = User.FindFirstValue(ClaimTypes.Email);

            // var user = await _userManager.FindByEmailAsync(email);

            var user = await _userManager.FindByEmailClaimsPrincipal(User);


            return new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
            };
        }

        [HttpGet("Emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
              return await _userManager.FindByEmailAsync(email)!=null;

        }

        [Authorize]
        [HttpGet("Address")]

        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            //var email = User.FindFirstValue(ClaimTypes.Email);
            //  var email = HttpContext.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)? .Value;
            var user = await _userManager.FindByEmailWithAddress(User);

            return _mapper.Map<Address,AddressDto>(user.Address);
        }
       [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto)
        {
            var user = await _userManager.FindByEmailWithAddress(User);
            user.Address = _mapper.Map<AddressDto, Address>(addressDto);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded) return Ok(_mapper.Map<Address,AddressDto>(user.Address));

            return BadRequest("Proplem Updatig this user");

        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user =await _userManager.FindByEmailAsync(loginDto.Email);

            if(user == null) return Unauthorized( new ApiResponse(401));

            var result = await _signInManager.
                CheckPasswordSignInAsync(user, loginDto.Password,false);

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return new UserDto{
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
            };
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>>Register(RegisterDto registerDto)
        {

            if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Emaill Address is in Use" } }); 
            }
            var user = new AppUser
            {
                DisplayName = registerDto.DisplyName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                PhoneNumber= registerDto.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return new UserDto
            {
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email
            };

        }



    }
}
