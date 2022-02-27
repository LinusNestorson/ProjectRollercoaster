namespace ProjectRollercoaster.Client.Services
{
    /// <summary>
    /// Handling refresh requests from client.
    /// </summary>
    public class RefreshService : IRefreshService
    {
        public event Action RefreshRequest;

        public void CallRequestRefresh()
        {
            RefreshRequest?.Invoke();
        }
    }
}