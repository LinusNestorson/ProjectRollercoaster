namespace ProjectRollercoaster.Server.Helpers
{
    using ProjectRollercoaster.Shared;

    /// <summary>
    /// Interface of UtilityHelper class.
    /// </summary>
    public interface IUtilityHelper
    {
        Task<User> GetUser();
    }
}