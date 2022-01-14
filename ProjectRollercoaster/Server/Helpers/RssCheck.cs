using Microsoft.EntityFrameworkCore;
using ProjectRollercoaster.Server.Data;
using System.ServiceModel.Syndication;
using System.Xml;

namespace ProjectRollercoaster.Server.Helpers
{
    public class RssCheck
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

        public async Task<bool> DoesRssExistInDb(string urlTest)
        {
            var dbFeed = await _context.Feeds.FirstOrDefaultAsync(f => f.RssLink == urlTest);
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