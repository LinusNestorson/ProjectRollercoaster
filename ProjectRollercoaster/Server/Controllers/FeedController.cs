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

    [Route("api/[controller]")]
    [ApiController]
    public class FeedController : ControllerBase
    {
        private readonly DataContext _context;

        public FeedController(DataContext context)
        {
            _context = context;
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
            RssHelper xmlHelpers = new();
            var check = xmlHelpers.IsRssValid(feed.Link);

            var dbFeed = await _context.Feeds.FirstOrDefaultAsync(f => f.Link == feed.Link);

            if (check && dbFeed == null)
            {
                var feedObject = xmlHelpers.GetRssInfo(feed.Link);
                _context.Feeds.Add(feedObject);
                await _context.SaveChangesAsync();
                return Ok(await _context.Feeds.ToListAsync());
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
            var dbFeeds = await _context.Feeds.ToListAsync();
            return Ok(dbFeeds);
        }
    }
}