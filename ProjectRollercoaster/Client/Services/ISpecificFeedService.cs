namespace ProjectRollercoaster.Client.Services
{
    using ProjectRollercoaster.Shared;

    public interface ISpecificFeedService
    {
        IList<Feed> SpecificFeeds { get; set; }

        Task GetSpecificFeed(string feedLink);
    }
}