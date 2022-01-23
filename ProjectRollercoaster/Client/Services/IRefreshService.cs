namespace ProjectRollercoaster.Client.Services
{
    public interface IRefreshService
    {
        event Action RefreshRequest;

        void CallRequestRefresh();
    }
}