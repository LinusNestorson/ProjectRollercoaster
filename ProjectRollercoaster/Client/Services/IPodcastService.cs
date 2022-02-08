namespace ProjectRollercoaster.Client.Services
{
    using ProjectRollercoaster.Shared;

    public interface IPodcastService
    {
        IList<Podcast> Podcasts { get; set; }

        event Action OnChange;

        Task<bool> AddPodcast(Podcast podcast);

        void RemovePodcast(int id);

        Task LoadAllPodcasts();
    }
}