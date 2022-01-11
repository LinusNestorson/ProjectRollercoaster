namespace ProjectRollercoaster.Client.Services
{
    using ProjectRollercoaster.Shared;
    using System.Net.Http.Json;
    using System.ServiceModel.Syndication;
    using System.Xml;

    public class FeedService : IFeedService
    {
        private readonly HttpClient _http;

        public FeedService(HttpClient http)
        {
            _http = http;
        }


        //public async Task GetFeeds()
        //{
        //    Feeds = await _http.GetFromJsonAsync<IList<Feed>>("api/feed");
        //}

        public void AddFeed(Feed feed)
        {


                string url = feed.RssLink;
                var reader = XmlReader.Create(url);


                _http.PostAsJsonAsync("api/feed", feed);


         
        }

        public void RemoveFeed(string rssLink)
        {
            _http.DeleteAsync("api/feed/" + rssLink);
        }
    }
}