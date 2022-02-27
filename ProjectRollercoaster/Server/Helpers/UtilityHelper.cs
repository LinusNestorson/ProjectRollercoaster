namespace ProjectRollercoaster.Server.Helpers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using ProjectRollercoaster.Server.Data;
    using ProjectRollercoaster.Shared;
    using System.Security.Claims;
    using System.Threading.Tasks;

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