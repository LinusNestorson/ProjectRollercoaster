namespace ProjectRollercoaster.Client.Services
{
    using ProjectRollercoaster.Shared;
    using System.Net.Http.Json;

    /// <summary>
    /// Service for handling requests regarding podcasts.
    /// Adding new podcasts, removing podcasts and Loading all podcasts on initialization.
    /// </summary>
    public class PodcastService : IPodcastService
    {
        private readonly HttpClient _http;

        public event Action OnChange;

        public IList<Podcast> Podcasts { get; set; } = new List<Podcast>();

        public PodcastService(HttpClient http)
        {
            _http = http;
        }

        /// <summary>
        /// Sends request to server for adding new podcast to database.
        /// </summary>
        /// <param name="podcast">Podcast object containing info about podcast from user.</param>
        /// <returns>True or false based on outcome.</returns>
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

        /// <summary>
        /// Sends remove request to server for removing podcast.
        /// </summary>
        /// <param name="id">Id of podcast to remove.</param>
        public void RemovePodcast(int id)
        {
            _http.DeleteAsync("api/podcast/" + id);
        }

        /// <summary>
        /// Loads all podcast into browser from server.
        /// </summary>
        /// <returns>Task with service respons from backend service.</returns>
        public async Task LoadAllPodcasts()
        {
            Podcasts = await _http.GetFromJsonAsync<IList<Podcast>>("api/podcast");
            OnChange?.Invoke();
        }
    }
}