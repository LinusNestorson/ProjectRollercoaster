namespace ProjectRollercoaster.Server.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using ProjectRollercoaster.Server.Data;
    using ProjectRollercoaster.Server.Helpers;
    using ProjectRollercoaster.Shared;
    using System.Security.Claims;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SpecificFeedController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUtilityHelper _utilityHelper;

        public SpecificFeedController(DataContext context, IUtilityHelper utilityHelper)
        {
            _context = context;
            _utilityHelper = utilityHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetFeedInfo(Feed feed)
        {
            RssHelper xmlHelpers = new();
            var check = xmlHelpers.IsRssValid(feed.Link);

            var dbFeed = await _context.Feeds.FirstOrDefaultAsync(f => f.Link == feed.Link);

            var listOfFeedObjects = xmlHelpers.GetRssContent(feed.Link);

            return Ok(listOfFeedObjects);
        }
    }
}