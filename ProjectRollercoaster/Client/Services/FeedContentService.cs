namespace ProjectRollercoaster.Client.Services
{
    using ProjectRollercoaster.Shared;
    using System.Net.Http.Json;

    public class FeedContentService : IFeedContentService
    {
        private readonly HttpClient _http;

        public IList<FeedContent> AllFeedContent { get; set; } = new List<FeedContent>();

        public FeedContentService(HttpClient http)
        {
            _http = http;
        }

        public async Task GetFeedContent()
        {
            AllFeedContent = await _http.GetFromJsonAsync<IList<FeedContent>>("api/feedcontent");
        }
    }
}