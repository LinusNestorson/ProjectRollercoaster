namespace ProjectRollercoaster.Server.Controllers
{
    using DocumentFormat.OpenXml.Office.CustomUI;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using ProjectRollercoaster.Shared;
    using System.ServiceModel.Syndication;
    using System.Xml;

    [Route("api/[controller]")]
    [ApiController]
    public class FeedController : ControllerBase
    {
        [HttpGet]
        public ActionResult<Feed> GetFeed()
        {
            var testObject = new Feed();

            string url = "http://svt.se/nyheter/regionalt/blekingenytt/rss.xml";
            var reader = XmlReader.Create(url);
            var feed = SyndicationFeed.Load(reader);
            foreach (SyndicationItem item in feed.Items)
            {
                testObject.PublishDate = item.PublishDate.ToString();
            }

            return Ok(testObject);
        }
    }
}