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

            XmlCheck xmlCheck = new();
            var check = await xmlCheck.DoesXmlExist(feed.RssLink);

            if (check == "success")
            {
                _context.Feeds.Add(feed);
                await _context.SaveChangesAsync();
                return Ok(await _context.Feeds.ToListAsync());
            }
            else
            { 
                return BadRequest();
            }

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
