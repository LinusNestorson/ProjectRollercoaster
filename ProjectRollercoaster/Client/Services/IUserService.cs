namespace ProjectRollercoaster.Client.Services
{
    using ProjectRollercoaster.Shared;

    /// <summary>
    /// Interface for UserService.
    /// </summary>
    public interface IUserService
    {
        Task<bool> RemoveUser();
    }
}