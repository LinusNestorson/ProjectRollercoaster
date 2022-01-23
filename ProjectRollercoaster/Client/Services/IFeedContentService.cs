namespace ProjectRollercoaster.Client.Services
{
    using ProjectRollercoaster.Shared;

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