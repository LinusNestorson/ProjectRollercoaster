namespace ProjectRollercoaster.Server.Helpers
{
    using ProjectRollercoaster.Shared;

    public interface IUtilityHelper
    {
        Task<User> GetUser();
    }
}