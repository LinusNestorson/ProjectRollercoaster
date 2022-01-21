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

        public event Action OnChange;

        public IList<Feed> Feeds { get; set; } = new List<Feed>();

        public FeedService(HttpClient http)
        {
            _http = http;
        }

        public async Task<bool> AddFeed(Feed feed)
        {
            var response = await _http.PostAsJsonAsync("api/feed", feed);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RemoveFeed(int id)
        {
            _http.DeleteAsync("api/feed/" + id);
        }

        public async Task LoadAllFeeds()
        {
            Feeds = await _http.GetFromJsonAsync<IList<Feed>>("api/feed");
            OnChange?.Invoke();
        }
    }
}