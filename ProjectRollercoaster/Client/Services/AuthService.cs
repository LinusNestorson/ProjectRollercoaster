namespace ProjectRollercoaster.Client.Services
{
    using ProjectRollercoaster.Shared;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    public class AuthService : IAuthService
    {
        private readonly IAuthService _authService;

        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ServiceResponse<int>> Register(UserRegister request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/register", request);

            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }
    }
}