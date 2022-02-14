namespace ProjectRollercoaster.Server.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using ProjectRollercoaster.Server.Data;
    using ProjectRollercoaster.Shared;
    using ProjectRollercoaster.Server.Helpers;
    using DocumentFormat.OpenXml.Office.CustomUI;
    using System.ServiceModel.Syndication;
    using System.Xml;
    using System.Security.Claims;

    /// <summary>
    /// Controller class for handling adding new and removing feeds.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FeedController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUtilityHelper _utilityHelper;

        public FeedController(DataContext context, IUtilityHelper utilityHelper)
        {
            _context = context;
            _utilityHelper = utilityHelper;
        }

        /// <summary>
        /// Adding new feed to database.
        /// </summary>
        /// <param name="feed">Feed info to add.</param>
        /// <returns>Status of request.</returns>
        [HttpPost]
        public async Task<IActionResult> AddFeed(Feed feed)
        {
            RssHelper rssHelper = new(_context);
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var check = rssHelper.IsRssValid(feed.Url, userId);
            await Task.Delay(1000);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var dbFeed = await _context.Feeds.FirstOrDefaultAsync(f => f.Url == feed.Url && f.User.Id == userId);

            if (check && dbFeed == null)
            {
                var feedObject = rssHelper.AddRssInfo(feed.Url, feed.Name, feed.Image, user);
                _context.Feeds.Add(feedObject);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Deleting existing feed from database.
        /// </summary>
        /// <param name="Id">Id of specific feed.</param>
        /// <returns>Status of request and sending back updated feed list</returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteFeed(int Id)
        {
            var dbFeed = await _context.Feeds.FirstOrDefaultAsync(f => f.Id == Id);
            if (dbFeed == null)
            {
                return NotFound("Feed with given link does not exist");
            }
            _context.Feeds.Remove(dbFeed);
            await _context.SaveChangesAsync();
            return Ok(await _context.Feeds.ToListAsync());
        }

        /// <summary>
        /// Handling request on getting all feeds from database.
        /// </summary>
        /// <returns>All feeds currently in database</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllFeeds()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var dbFeeds = await _context.Feeds.Where(f => f.User.Id == userId).ToListAsync();
            return Ok(dbFeeds);
        }
    }
}