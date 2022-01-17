namespace ProjectRollercoaster.Server.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using ProjectRollercoaster.Server.Data;

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetUser()
        //{
        //    var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == 1);
        //    return Ok(user);
        //}
    }

    //[HttpGet]
}