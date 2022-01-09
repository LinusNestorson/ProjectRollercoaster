namespace ProjectRollercoaster.Client.Services
{
    using ProjectRollercoaster.Shared;

    public interface IFeedService
    {
        Task<Feed> GetFeed();
    }
}