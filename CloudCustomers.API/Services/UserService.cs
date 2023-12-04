using CloudCustomers.API.Config;
using CloudCustomers.API.Model;
using Microsoft.Extensions.Options;

namespace CloudCustomers.API.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly UsersApiOptions _apiConfig;

        public UserService(HttpClient httpClient,IOptions<UsersApiOptions> apiConfig)
        {
            _httpClient = httpClient;
            _apiConfig = apiConfig.Value;
        }
        public async Task<List<User>> GetAllUsers()
        {
            var userResponse = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");
            if (userResponse.StatusCode==System.Net.HttpStatusCode.NotFound) {
            return new List<User>();
            }
            var responsContent=userResponse.Content;
            var allUsers = await responsContent.ReadFromJsonAsync<List<User>>();
            return allUsers.ToList();
            //return new List<User>() { };
        }
    }
}
