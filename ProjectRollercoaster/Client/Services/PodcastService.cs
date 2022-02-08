namespace ProjectRollercoaster.Client.Services
{
    using ProjectRollercoaster.Shared;
    using System.Net.Http.Json;

    public class PodcastService : IPodcastService
    {
        private readonly HttpClient _http;

        public event Action OnChange;

        public IList<Podcast> Podcasts { get; set; } = new List<Podcast>();

        public PodcastService(HttpClient http)
        {
            _http = http;
        }

        public async Task<bool> AddPodcast(Podcast podcast)
        {
            var response = await _http.PostAsJsonAsync("api/podcast", podcast);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RemovePodcast(int id)
        {
            _http.DeleteAsync("api/podcast/" + id);
        }

        public async Task LoadAllPodcasts()
        {
            Podcasts = await _http.GetFromJsonAsync<IList<Podcast>>("api/podcast");
            OnChange?.Invoke();
        }
    }
}