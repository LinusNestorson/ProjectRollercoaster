using Microsoft.EntityFrameworkCore;
using ProjectRollercoaster.Server.Data;
using ProjectRollercoaster.Shared;
using System.ServiceModel.Syndication;
using System.Xml;

namespace ProjectRollercoaster.Server.Helpers
{
    public class RssHelper

    {
        private readonly DataContext _context;

        public bool IsRssValid(string urlTest)
        {
            if (DoesRssExist(urlTest))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DoesRssExist(string urlTest)
        {
            try
            {
                var reader = XmlReader.Create(urlTest);
                var rssfeed = SyndicationFeed.Load(reader);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public Feed GetRssInfo(string url)
        {
            Feed feedObject = new();

            feedObject.Link = url;
            var reader = XmlReader.Create(url);
            var feed = SyndicationFeed.Load(reader);
            foreach (SyndicationItem item in feed.Items)
            {
                feedObject.Title = item.Title.Text;
                feedObject.Content = item.Summary.Text;
                feedObject.PublishDate = item.PublishDate.ToString();
            }

            return feedObject;
        }

        public async Task<bool> DoesRssExistInDb(string urlTest)
        {
            var dbFeed = await _context.Feeds.FirstOrDefaultAsync(f => f.Link == urlTest);
            if (dbFeed != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}