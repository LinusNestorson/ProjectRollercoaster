namespace ProjectRollercoaster.Client.Services
{
    using ProjectRollercoaster.Shared;
    using System.Net.Http.Json;

    public class SpecificFeedService : ISpecificFeedService
    {
        private readonly HttpClient _http;
        private readonly IFeedContentService _feedContent;

        public FeedDTO TempInfo { get; set; } = new FeedDTO();

        public IList<FeedContent> SpecificFeedContent { get; set; } = new List<FeedContent>();

        //public List<List<FeedContent>> ListOfSpecificFeedContent { get; set; } = new List<List<FeedContent>>();

        public SpecificFeedService(HttpClient http, IFeedContentService feedContentService)
        {
            _http = http;
            _feedContent = feedContentService;
        }

        //public async Task GetSpecificFeedContent(int id)
        //{
        //    SpecificFeedContent = await _http.GetFromJsonAsync<IList<FeedContent>>("api/feedcontent/" + id);
        //}

        public void GetSpecificFeedContent(int id)
        {
            SpecificFeedContent = _feedContent.combinedFeedList.Where(f => f.Id == id).ToList();
        }

        public void SetTempInfoId(int id, string name)
        {
            TempInfo.Id = id;
            TempInfo.Name = name;
        }
    }
}