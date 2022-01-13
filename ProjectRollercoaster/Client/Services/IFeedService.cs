namespace ProjectRollercoaster.Client.Services
{
    using Microsoft.AspNetCore.Mvc;
    using ProjectRollercoaster.Shared;

    public interface IFeedService
    {
        IList<Feed> Feeds { get; set; }

        event Action OnChange;

        Task<bool> AddFeed(Feed rssLink);

        void RemoveFeed(int id);

        Task LoadAllFeeds();
    }
}