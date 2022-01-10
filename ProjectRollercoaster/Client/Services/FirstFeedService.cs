namespace ProjectRollercoaster.Client.Services
{
    using ProjectRollercoaster.Shared;
    using System.Net.Http.Json;

    public class FirstFeedService : IFeedService
    {
        private readonly HttpClient _http;

        public FirstFeedService(HttpClient http)
        {
            _http = http;
        }

        public async Task<Feed> GetFeed()
        {
            var feed = await _http.GetFromJsonAsync<Feed>("api/feed");
            return feed;
        }
    }
}