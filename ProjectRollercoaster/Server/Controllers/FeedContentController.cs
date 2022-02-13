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

    /// <summary>
    /// Controller handling requests regarding feed content.
    /// </summary>
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

        /// <summary>
        /// Default get controller handling feed content.
        /// </summary>
        /// <returns>Content of all feeds</returns>
        [HttpGet()]
        public async Task<IActionResult> GetFeedInfo()
        {
            RssHelper rssHelper = new(_context);

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var listOfFeeds = await _context.Feeds.Where(f => f.User.Id == userId).ToListAsync();

            var listOfFeedObjectContent = rssHelper.GetRssContent(listOfFeeds);

            return Ok(listOfFeedObjectContent);
        }

        /// <summary>
        /// Gets the content of a specific feed.
        /// </summary>
        /// <param name="id">Id if wanted feed.</param>
        /// <returns>List of feeds from specific feed.</returns>
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