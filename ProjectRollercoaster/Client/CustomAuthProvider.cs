namespace ProjectRollercoaster.Client
{
    using Blazored.LocalStorage;
    using Microsoft.AspNetCore.Components.Authorization;
    using System.Net.Http.Headers;
    using System.Security.Claims;
    using System.Text.Json;
    using System.Threading.Tasks;

    /// <summary>
    /// Handling JWT and user authentication.
    /// </summary>
    public class CustomAuthStateProvider : AuthenticationStateProvider

    {
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _http;

        public CustomAuthStateProvider(ILocalStorageService localStorageService, HttpClient http)
        {
            _localStorageService = localStorageService;
            _http = http;
        }

        /// <summary>
        /// Parses the authentication token and alerts compononent if the user is authenticated or not.
        /// </summary>
        /// <returns>Authentication state of the user</returns>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string authToken = await _localStorageService.GetItemAsStringAsync("authToken");

            var identity = new ClaimsIdentity();
            _http.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrEmpty(authToken))
            {
                identity = new ClaimsIdentity(ParseClaimsFromJwt(authToken), "jwt");
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken.Replace("\"", ""));
            }
            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }

        /// <summary>
        /// Parse incoming base64 string to a base8 string.
        /// </summary>
        /// <param name="base64">Token as base64 string.</param>
        /// <returns>Returns 8-bit array.</returns>
        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        /// <summary>
        /// Parses claims from Jwt.
        /// </summary>
        /// <param name="jwt">Parses the claims from jwt containing info of user.</param>
        /// <returns>Claims of user.</returns>
        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            var claims = keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));

            return claims;
        }
    }
}