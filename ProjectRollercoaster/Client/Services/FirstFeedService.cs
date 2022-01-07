namespace ProjectRollercoaster.Client.Services
{
    using System.Net.Http.Json;

    public class FirstFeedService : IFeedService
    {
        private readonly HttpClient _http;

        public string FirstFeed { get; set; }

        public FirstFeedService(HttpClient http)
        {
            _http = http;
        }

        public async Task GetFirstFeed()
        {
            FirstFeed = await _http.GetFromJsonAsync<string>("api/user/getfirstfeed");
        }
    }
}