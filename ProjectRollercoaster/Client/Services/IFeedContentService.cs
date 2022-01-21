namespace ProjectRollercoaster.Client.Services
{
    using ProjectRollercoaster.Shared;

    public interface IFeedContentService
    {
        IList<FeedContent> AllFeedContent { get; set; }

        Task GetFeedContent();
    }
}