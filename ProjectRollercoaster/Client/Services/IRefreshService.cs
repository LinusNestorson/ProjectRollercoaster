namespace ProjectRollercoaster.Client.Services
{
    /// <summary>
    /// Interface for RefreshService.
    /// </summary>
    public interface IRefreshService
    {
        event Action RefreshRequest;

        void CallRequestRefresh();
    }
}