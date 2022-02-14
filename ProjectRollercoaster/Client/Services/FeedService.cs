namespace ProjectRollercoaster.Client.Services
{
    using Microsoft.AspNetCore.Mvc;
    using ProjectRollercoaster.Shared;
    using System.Net.Http.Json;
    using System.ServiceModel.Syndication;
    using System.Xml;

    /// <summary>
    /// Contains the request related to "Feeds" page. Handling adding new and removing feeds.
    /// </summary>
    public class FeedService : IFeedService
    {
        private readonly HttpClient _http;

        public event Action OnChange;

        public IList<Feed> Feeds { get; set; } = new List<Feed>();

        public FeedService(HttpClient http)
        {
            _http = http;
        }

        /// <summary>
        /// Sends request to server to add new feed to database.
        /// </summary>
        /// <param name="feed">Feed object containing information about feed</param>
        /// <returns>True or false based on outcome.</returns>
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

        /// <summary>
        /// Sends request to server to remove feed from database.
        /// </summary>
        /// <param name="id">Id of feed to remove from database.</param>
        public void RemoveFeed(int id)
        {
            _http.DeleteAsync("api/feed/" + id);
        }

        /// <summary>
        /// Gets all feeds currently in the database.
        /// </summary>
        /// <returns>Task with service respons from backend service.</returns>
        public async Task LoadAllFeeds()
        {
            Feeds = await _http.GetFromJsonAsync<IList<Feed>>("api/feed");
            OnChange?.Invoke();
        }
    }
}