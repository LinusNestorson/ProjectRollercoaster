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
        private readonly RssHelper _helper;

        public FeedContentController(DataContext context, IUtilityHelper utilityHelper)
        {
            _context = context;
            _utilityHelper = utilityHelper;
        }

        [HttpGet()]
        public async Task<IActionResult> GetFeedInfo()
        {
            RssHelper rssHelper = new(_context);

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var listOfFeeds = await _context.Feeds.Where(f => f.User.Id == userId).ToListAsync();

            var listOfFeedObjectContent = rssHelper.GetRssContent(listOfFeeds);

            return Ok(listOfFeedObjectContent);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpecificFeed(int id)
        {
            RssHelper rssHelper = new(_context);

            var feed = await _context.Feeds.FirstOrDefaultAsync(f => f.Id == id);


            var listFeedContent = rssHelper.GetSpecificRssContent(feed.Url);

            return Ok(listFeedContent);
        }
    }
}