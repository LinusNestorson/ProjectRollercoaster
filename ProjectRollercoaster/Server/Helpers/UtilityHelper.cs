namespace ProjectRollercoaster.Server.Helpers
{
    using ProjectRollercoaster.Server.Data;
    using ProjectRollercoaster.Shared;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using System.Security.Claims;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Helper class handling communication between client and server.
    /// </summary>
    public class UtilityHelper : IUtilityHelper
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UtilityHelper(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Parses information about user from claims.
        /// </summary>
        /// <returns>User object with information from claims.</returns>
        public async Task<User> GetUser()
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }
    }
}