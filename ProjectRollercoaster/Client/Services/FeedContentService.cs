namespace ProjectRollercoaster.Client.Services
{
    using ProjectRollercoaster.Shared;
    using System.Net.Http.Json;

    /// <summary>
    /// Class handling the content of news feeds and sends request to server.
    /// Saves content to list that are injected into the pages.
    /// </summary>
    public class FeedContentService : IFeedContentService
    {
        private readonly HttpClient _http;

        public IList<FeedContent> AllFeedContent { get; set; } = new List<FeedContent>();

        public List<List<FeedContent>> ListOfSpecificFeedContent { get; set; } = new List<List<FeedContent>>();

        public List<FeedContent> AllFeedsBasedOnPublish { get; set; } = new List<FeedContent>();

        public List<FeedContent> combinedFeedList { get; set; } = new();

        public FeedContentService(HttpClient http)
        {
            _http = http;
        }

        /// <summary>
        /// Sends request to server in order to fill lists with content.
        /// </summary>
        /// <returns>Task with service respons from backend service.</returns>
        public async Task GetFeedContent()
        {
            combinedFeedList.Clear();
            ListOfSpecificFeedContent = await _http.GetFromJsonAsync<List<List<FeedContent>>>("api/feedcontent");
            AddAllFeedsToOnelIst();
        }

        /// <summary>
        /// Adds together all feeds from seperate lists into one list.
        /// </summary>
        public void AddAllFeedsToOnelIst()
        {
            foreach (var feedList in ListOfSpecificFeedContent)
            {
                foreach (var feed in feedList)
                {
                    combinedFeedList.Add(feed);
                }
            }

            SortFromPublish();
        }

        /// <summary>
        /// Sort the lists based on publish time of news.
        /// </summary>
        public void SortFromPublish()
        {
            AllFeedsBasedOnPublish = combinedFeedList.OrderByDescending(p => p.PublishDate).ToList();
        }
    }
}