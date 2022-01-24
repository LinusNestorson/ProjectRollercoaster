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
    public class FeedContentController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUtilityHelper _utilityHelper;

        public FeedContentController(DataContext context, IUtilityHelper utilityHelper)
        {
            _context = context;
            _utilityHelper = utilityHelper;
        }

        [HttpGet()]
        public async Task<IActionResult> GetFeedInfo()
        {
            RssHelper xmlHelpers = new();

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var listOfFeeds = await _context.Feeds.Where(f => f.User.Id == userId).ToListAsync();

            var listOfFeedObjectContent = xmlHelpers.GetRssContent(listOfFeeds);

            return Ok(listOfFeedObjectContent);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpecificFeed(int id)
        {
            var feed = await _context.Feeds.FirstOrDefaultAsync(f => f.Id == id);

            RssHelper xmlHelpers = new();

            var listFeedContent = xmlHelpers.GetSpecificRssContent(feed.Url);

            return Ok(listFeedContent);
        }
    }
}