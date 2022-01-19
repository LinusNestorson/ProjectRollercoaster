namespace ProjectRollercoaster.Client.Services
{
    using ProjectRollercoaster.Shared;
    using System.Net.Http.Json;

    public class SpecificFeedService : ISpecificFeedService
    {
        private readonly HttpClient _http;

        public IList<Feed> SpecificFeeds { get; set; } = new List<Feed>();

        public SpecificFeedService(HttpClient http)
        {
            _http = http;
        }

        public async Task GetSpecificFeed(string feedLink)
        {
            SpecificFeeds = await _http.GetFromJsonAsync<IList<Feed>>("api/specificfeed" + feedLink);
        }
    }
}