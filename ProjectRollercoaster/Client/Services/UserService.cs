namespace ProjectRollercoaster.Client.Services
{
    using Microsoft.AspNetCore.Mvc;
    using ProjectRollercoaster.Shared;
    using System.Net.Http.Json;

    /// <summary>
    /// Service for handling remove user requests to server.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly HttpClient _http;

        public UserService(HttpClient http)
        {
            _http = http;
        }

        /// <summary>
        /// Sends remove request to server.
        /// </summary>
        /// <returns>Returns true or false based on outcome.</returns>
        public async Task<bool> RemoveUser()
        {
            var response = await _http.DeleteAsync("api/user");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else return false;
        }
    }
}