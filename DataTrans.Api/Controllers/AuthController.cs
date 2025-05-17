using DataTrans.Application.Common.Handler;
using DataTrans.Application.Dtos.Request;
using DataTrans.Application.Dtos.Response;
using DataTrans.Application.Services.Interfaces;
using DataTrans.Domain.Entities;
using DataTrans.Infustracture.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnMapper;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DataTrans.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly OnMapping _mapper;
        private readonly ILogger<AuthController> _logger;
        private readonly ApplicationDbContext _context;
        public AuthController(IUserService service,
            OnMapping mapper,
            ILogger<AuthController> logger,
            ApplicationDbContext context
            )
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }


        [HttpGet]
        public async Task<Result<IEnumerable<SystemUser>>> Get()
        {
            try
            {
                var user = User.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name)?.Value;

                _logger.LogInformation($"User {user} is attempting to retrieve all user records");

                var result = await _service.GetAllAsync();

                _logger.LogInformation($"User {user} successfully retrieved all user records");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while fetching users: {ex.Message}");
                throw;
            }
        }


        //[Authorize]
        //[HttpGet("getUser/{userId}")]
        //public async Task<Reservationpitch.Application.Common.Handling.Result<UserResponseDto>> Get(string userId)
        //{
        //    var result = await _service.GetByIdAsync(userId);

        //    return await Reservationpitch.Application.Common.Handling.Result<UserResponseDto>.SuccessAsync(result.Data, "User is found Successfully", true);
        //}

        [HttpGet("getUserRoles/{userId}")]
        public async Task<Result<IList<string>>> GetUserRoles(string userId)
        {
            List<string> userrolesList = [];
            var userRoles = await _context.UserRoles.Where(ur => ur.UserId == userId).ToListAsync();

            foreach (var role in userRoles)
            {
                var roles = await _context.Roles.Where(r => r.Id == role.RoleId).ToListAsync();
                foreach (var item in roles)
                {
                    userrolesList.Add(item.Name);
                }
            }

            // To implement the same however using entity framework LinQ 
            //userRoles = await (from ur in _context.UserRoles
            //                       join r in _context.Roles
            //                       on ur.RoleId equals r.Id
            //                       where ur.UserId == userId
            //                       select r.Name)
            //                       .ToListAsync();

            return await Result<IList<string>>.SuccessAsync(userrolesList, "Get All UserRoles successfully");
        }

        [HttpPost]
        [Route("login")]
        public async Task<Result<LoginResponseDto>> Login([FromBody] LoginRequestDto request)
        {
            var response = await _service.Login(request);

            if (response.IsSuccess)
            {
                return await Result<LoginResponseDto>.SuccessAsync(response.Data, "Logged Successfully", true);
            }

            return await Result<LoginResponseDto>.FaildAsync(false, "Username or Password are incorrect");
        }

        [HttpPost]
        [Route("register")]
        public async Task<Result<UserResponseDto>> Register([FromBody] RegisterRequestDto request)
        {

            //if (request.file == null)
            //{
            //    //return BadRequest(new { message = "The file field is required." });
            //    throw new Exception();
            //}

            //var imageUrl = await _imageService.UploadImage(request, request.file);

            //request.ImageUrl = imageUrl.Data;

            var response = await _service.Register(request);


            if (response.IsSuccess)
            {

                var mappedUser = await _mapper.Map<UserResponseDto, SystemUser>(response.Data);
                var code = await _service.GenerateUserEmailConfirmationTokenAsync(mappedUser.Data);
                //var callbackUrl = Url.Action("ConfirmEmail", "Auth", new
                //{
                //    userid = mappedUser.Data.Id,
                //    code
                //}, protocol: HttpContext.Request.Scheme);


                ////Method for sending email to 
                //await _emailSender.SendEmailAsync(request.Email, "Confirm Email",
                //    $"Please confirm your email by clicking here : <a href='{callbackUrl}'>Link</a>");
                return await Result<UserResponseDto>.SuccessAsync(response.Data, "Account is Created Successfully", true);
            }

            return await Result<UserResponseDto>.FaildAsync(false, "Account is already in use");
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<Result<UserResponseDto>> GetCurrentUserProfile()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    return await Result<UserResponseDto>.FaildAsync(false, "User not found from token");
                }

                var result = await _service.GetByIdAsync(userId);

                if (result.IsSuccess)
                {
                    return await Result<UserResponseDto>.SuccessAsync(result.Data, "Profile fetched successfully", true);
                }

                return await Result<UserResponseDto>.FaildAsync(false, "Profile not found");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching user profile: {ex.Message}");
                return await Result<UserResponseDto>.FaildAsync(false, "An error occurred while fetching the profile");
            }
        }


        [HttpPost]
        [Route("roleAssign")]
        public async Task<Result<bool>> UserRole([FromBody] UserRoleRequestDto request)
        {
            var response = await _service.AddUserToRole(request);
            if (response.IsSuccess)
            {
                return await Result<bool>.SuccessAsync(response.Data, "Role is Added Successfully", true);
            }

            return await Result<bool>.FaildAsync(false, "Role not added");
        }


        [HttpPost]
        [Route("refresh")]

        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto request)
        {
            var response = await _service.RefreshTokenAsync(request);

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.Message);
        }

    }
}
