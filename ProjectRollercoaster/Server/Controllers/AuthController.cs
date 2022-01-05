namespace ProjectRollercoaster.Server.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using ProjectRollercoaster.Server.Data;
    using ProjectRollercoaster.Shared;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

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
    }
}