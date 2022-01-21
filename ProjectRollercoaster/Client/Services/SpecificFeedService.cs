namespace ProjectRollercoaster.Client.Services
{
    using ProjectRollercoaster.Shared;
    using System.Net.Http.Json;

    public class SpecificFeedService : ISpecificFeedService
    {
        private readonly HttpClient _http;

        public FeedDTO TempInfo { get; set; } = new FeedDTO();

        public IList<FeedContent> SpecificFeedContent { get; set; } = new List<FeedContent>();

        //public List<List<FeedContent>> ListOfSpecificFeedContent { get; set; } = new List<List<FeedContent>>();

        public SpecificFeedService(HttpClient http)
        {
            _http = http;
        }

        public async Task GetSpecificFeedContent(int id)
        {
            SpecificFeedContent = await _http.GetFromJsonAsync<IList<FeedContent>>("api/feedcontent/" + id);
        }

        public void SetTempInfoId(int id, string name)
        {
            TempInfo.Id = id;
            TempInfo.Name = name;
        }
    }
}