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

        //[HttpGet]
        //public ActionResult<string> CheckFeed(string feedurl)
        //{
        //string testObject = "";

        //string url = "http://svt.se/nyheter/regionalt/blekingenytt/rss.xml";
        //var reader = XmlReader.Create(url);
        //var feed = SyndicationFeed.Load(reader);
        //    foreach (SyndicationItem item in feed.Items)
        //    {
        //        testObject = item.PublishDate.ToString();
        //    }

        //    return Ok(testObject);

        //}

        //[HttpGet]
        //public async Task<IActionResult> GetFeeds()
        //{
        //    return Ok()
        //}

        [HttpPost]
        public async Task<IActionResult> AddFeed(Feed feed)
        {
            RssHelper rssHelper = new(_context);
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var check = rssHelper.IsRssValid(feed.Url, userId);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            var dbFeed = await _context.Feeds.FirstOrDefaultAsync(f => f.Url == feed.Url);

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

        [HttpGet]
        public async Task<IActionResult> GetAllFeeds()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var dbFeeds = await _context.Feeds.Where(f => f.User.Id == userId).ToListAsync();
            return Ok(dbFeeds);
        }
    }
}