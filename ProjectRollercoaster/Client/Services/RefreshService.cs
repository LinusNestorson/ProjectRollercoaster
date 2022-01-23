namespace ProjectRollercoaster.Client.Services
{
    public class RefreshService : IRefreshService
    {
        public event Action RefreshRequest;

        public void CallRequestRefresh()
        {
            RefreshRequest?.Invoke();
        }
    }
}