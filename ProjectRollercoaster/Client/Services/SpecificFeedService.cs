namespace ProjectRollercoaster.Client.Services
{
    using ProjectRollercoaster.Shared;
    using System.Net.Http.Json;

    /// <summary>
    /// Service handling feeds on the specific feed page containing only feeds from one source.
    /// </summary>
    public class SpecificFeedService : ISpecificFeedService
    {
        private readonly HttpClient _http;
        private readonly IFeedContentService _feedContent;

        public FeedDTO TempInfo { get; set; } = new FeedDTO();

        public IList<FeedContent> SpecificFeedContent { get; set; } = new List<FeedContent>();

        public SpecificFeedService(HttpClient http, IFeedContentService feedContentService)
        {
            _http = http;
            _feedContent = feedContentService;
        }

        /// <summary>
        /// Gets the specific feed from list of all feeds.
        /// </summary>
        /// <param name="id">Id of the specific feed.</param>
        public void GetSpecificFeedContent(int id)
        {
            SpecificFeedContent = _feedContent.combinedFeedList.Where(f => f.Id == id).ToList();
        }

        /// <summary>
        /// Sets up temporary info of feed while being passed over to another method.
        /// </summary>
        /// <param name="id">Temporary Id of feed</param>
        /// <param name="name">Temporary name of feed</param>
        public void SetTempInfoId(int id, string name)
        {
            TempInfo.Id = id;
            TempInfo.Name = name;
        }
    }
}