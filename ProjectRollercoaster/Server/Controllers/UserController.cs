namespace ProjectRollercoaster.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using ProjectRollercoaster.Server.Data;
    using System.Security.Claims;

    /// <summary>
    /// Controller class handling requests regarding user from user settings.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        //private readonly IUserHelper _userHelp;
        private readonly DataContext _context;

        public UserController(IAuthRepository authRepo, DataContext context)
        {
            _authRepo = authRepo;
            _context = context;
        }

        /// <summary>
        /// Handling delete request from user.
        /// </summary>
        /// <returns>Status of request.</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteUser()
        {
            var user = await (_context.Users.FirstOrDefaultAsync(u => u.Id == int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))));

            var feeds = await _context.Feeds.Where(f => f.User == user).ToListAsync();

            var podcasts = await _context.Podcasts.Where(f => f.User == user).ToListAsync();

            if (user is not null)
            {
                _context.Users.Remove(user);
                foreach (var feed in feeds)
                {
                    _context.Feeds.Remove(feed);
                }
                foreach (var podcast in podcasts)
                {
                    _context.Podcasts.Remove(podcast);
                }

                await _context.SaveChangesAsync();
                return Ok();
            }
            else return BadRequest();
        }
    }
}