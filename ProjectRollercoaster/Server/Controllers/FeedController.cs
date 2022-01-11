namespace ProjectRollercoaster.Server.Controllers
{
    using DocumentFormat.OpenXml.Office.CustomUI;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using ProjectRollercoaster.Server.Data;
    using ProjectRollercoaster.Shared;
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
        //public ActionResult<Feed> GetFeed()
        //{
        //    var testObject = new Feed();

        //        string url = "http://svt.se/nyheter/regionalt/blekingenytt/rss.xml";
        //        var reader = XmlReader.Create(url);
        //        var feed = SyndicationFeed.Load(reader);
        //        foreach (SyndicationItem item in feed.Items)
        //        {
        //            testObject.PublishDate = item.PublishDate.ToString();
        //            testObject.Summary = item.Summary.Text;
        //        }

        //    return Ok(testObject);
        //    }
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetFeeds()
        //{
        //    return Ok()
        //}

        [HttpPost]
        public async Task<IActionResult> AddFeed(Feed feed)
        {
            _context.Feeds.Add(feed);
            await _context.SaveChangesAsync();
            return Ok(await _context.Feeds.ToListAsync());
        }

        [HttpDelete("{rsslink}")]
        public async Task<IActionResult> DeleteFeed(string rsslink)
        {
            var dbFeed = await _context.Feeds.FirstOrDefaultAsync(f => f.RssLink == rsslink);
            if (dbFeed == null)
            {
                return NotFound("Feed with given link does not exist");
            }
            _context.Feeds.Remove(dbFeed);
            await _context.SaveChangesAsync();
            return Ok(await _context.Feeds.ToListAsync());
        }
    }
}
