namespace ProjectRollercoaster.Client.Services
{
    using ProjectRollercoaster.Shared;

    public interface IUserService
    {
        Task<bool> RemoveUser();
    }
}