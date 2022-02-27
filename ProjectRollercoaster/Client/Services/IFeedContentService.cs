namespace ProjectRollercoaster.Client.Services
{
    using ProjectRollercoaster.Shared;

    /// <summary>
    /// Interface for FeedContentService.
    /// </summary>
    public interface IFeedContentService
    {
        IList<FeedContent> AllFeedContent { get; set; }

        List<List<FeedContent>> ListOfSpecificFeedContent { get; set; }

        List<FeedContent> AllFeedsBasedOnPublish { get; set; }

        List<FeedContent> combinedFeedList { get; set; }

        Task GetFeedContent();

        void AddAllFeedsToOnelIst();

        void SortFromPublish();
    }
}