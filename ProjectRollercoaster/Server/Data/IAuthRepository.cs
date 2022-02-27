namespace ProjectRollercoaster.Server.Data
{
    using ProjectRollercoaster.Shared;

    /// <summary>
    /// Interface of AuthRepository class.
    /// </summary>
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);

        Task<ServiceResponse<string>> Login(string email, string password);

        Task<bool> UserExists(string email);
    }
}