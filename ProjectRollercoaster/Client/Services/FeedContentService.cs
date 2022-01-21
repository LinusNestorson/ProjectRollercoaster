namespace ProjectRollercoaster.Client.Services
{
    using ProjectRollercoaster.Shared;
    using System.Net.Http.Json;

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

        public async Task GetFeedContent()
        {
            combinedFeedList.Clear();
            ListOfSpecificFeedContent = await _http.GetFromJsonAsync<List<List<FeedContent>>>("api/feedcontent");
            AddAllFeedsToOnelIst();
        }

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

        public void SortFromPublish()
        {
            AllFeedsBasedOnPublish = combinedFeedList.OrderByDescending(p => p.PublishDate).ToList();
        }
    }
}