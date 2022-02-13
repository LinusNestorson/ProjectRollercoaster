namespace ProjectRollercoaster.Client.Services
{
    using Microsoft.AspNetCore.Mvc;
    using ProjectRollercoaster.Shared;

    /// <summary>
    /// Interface for FeedService.
    /// </summary>
    public interface IFeedService
    {
        IList<Feed> Feeds { get; set; }

        event Action OnChange;

        Task<bool> AddFeed(Feed feed);

        void RemoveFeed(int id);

        Task LoadAllFeeds();
    }
}