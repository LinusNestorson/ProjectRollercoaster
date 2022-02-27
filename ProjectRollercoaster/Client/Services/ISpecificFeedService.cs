namespace ProjectRollercoaster.Client.Services
{
    using ProjectRollercoaster.Shared;

    /// <summary>
    /// Interface for SpecificFeedService.
    /// </summary>
    public interface ISpecificFeedService
    {
        IList<FeedContent> SpecificFeedContent { get; set; }

        FeedDTO TempInfo { get; set; }

        void GetSpecificFeedContent(int id);

        void SetTempInfoId(int id, string name);
    }
}