namespace ProjectRollercoaster.Client.Services
{
    using ProjectRollercoaster.Shared;

    public interface ISpecificFeedService
    {
        IList<FeedContent> SpecificFeedContent { get; set; }

        FeedDTO TempInfo { get; set; }

        Task GetSpecificFeedContent(int id);

        void SetTempInfoId(int id, string name);
    }
}