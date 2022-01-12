namespace ProjectRollercoaster.Client.Services
{
    using Microsoft.AspNetCore.Mvc;
    using ProjectRollercoaster.Shared;

    public interface IFeedService
    {
        //Task<Feed> GetFeed();
        //Task GetFeed(Feed rssLink);

        Task<bool> AddFeed(Feed rssLink);

        void RemoveFeed(string rssLink);
    }
}