namespace ProjectRollercoaster.Client.Services
{
    using ProjectRollercoaster.Shared;

    /// <summary>
    /// Interface for AuthService.
    /// </summary>
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserRegister request);

        Task<ServiceResponse<string>> Login(UserLogin request);
    }
}