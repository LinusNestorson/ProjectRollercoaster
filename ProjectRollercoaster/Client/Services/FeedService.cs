namespace ProjectRollercoaster.Client.Services
{
    using Microsoft.AspNetCore.Mvc;
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


        //public async Task GetFeed(Feed feed)
        //{
        //    var response = await _http.GetFromJsonAsync<string>("api/feed/" + feed.RssLink);

        //    if (response == "testSuccess")
        //    {
        //        AddFeed(feed);
        //    }
        //    else if (response == "testFail")
        //    {
        //        throw new Exception("TestFail");
        //    }


        //}

        public async Task<ActionResult<bool>> AddFeed(Feed feed)
        {

           var response = await _http.PostAsJsonAsync("api/feed", feed);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void RemoveFeed(string rssLink)
        {
            _http.DeleteAsync("api/feed/" + rssLink);
        }
    }
}