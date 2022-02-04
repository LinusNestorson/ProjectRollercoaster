namespace ProjectRollercoaster.Client.Services
{
    using Microsoft.AspNetCore.Mvc;
    using ProjectRollercoaster.Shared;
    using System.Net.Http.Json;

    public class UserService : IUserService
    {
        private readonly HttpClient _http;

        public UserService(HttpClient http)
        {
            _http = http;
        }

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