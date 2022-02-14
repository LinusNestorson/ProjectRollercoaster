namespace ProjectRollercoaster.Server.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using ProjectRollercoaster.Server.Data;
    using ProjectRollercoaster.Shared;

    /// <summary>
    /// Authentication controller handling requests about user authentication.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        /// <summary>
        /// Controller handling register requests.
        /// </summary>
        /// <param name="request">User register input.</param>
        /// <returns>Returns request status.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegister request)
        {
            var response = await _authRepo.Register(
                new User
                {
                    Username = request.Username,
                    Email = request.Email,
                    IsConfirmed = request.IsConfirmed
                }, request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Controller handling login requests.
        /// </summary>
        /// <param name="request">User login input.</param>
        /// <returns>Returns request status.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLogin request)
        {
            var response = await _authRepo.Login(
                request.Email, request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}