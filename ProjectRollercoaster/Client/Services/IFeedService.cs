namespace ProjectRollercoaster.Client.Services
{
    using ProjectRollercoaster.Shared;

    public interface IFeedService
    {
        //Task<Feed> GetFeed();

        void AddFeed(Feed rssLink);

        void RemoveFeed(string rssLink);
    }
}