namespace ProjectRollercoaster.Client.Services
{
    using ProjectRollercoaster.Shared;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    /// <summary>
    /// Service class handling login and registration used to communicate with backend.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;
        }

        /// <summary>
        /// Sends a request with login details from user.
        /// </summary>
        /// <param name="request">Request containing user email and password</param>
        /// <returns>Task with service respons from backend service</returns>
        public async Task<ServiceResponse<string>> Login(UserLogin request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/login", request);

            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        /// <summary>
        /// Send request with register information from user.
        /// </summary>
        /// <param name="request">Request containing email, username and password</param>
        /// <returns>Task with service respons from backend service</returns>
        public async Task<ServiceResponse<int>> Register(UserRegister request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/register", request);

            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }
    }
}