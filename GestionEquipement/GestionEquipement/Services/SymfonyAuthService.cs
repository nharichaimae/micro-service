using GestionEquipement.DTO;

namespace GestionEquipement.Services
{
    public class SymfonyAuthService
    {
        private readonly HttpClient _httpClient;

        public SymfonyAuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

    
        public async Task<int?> GetUserIdAsync(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var response = await _httpClient.GetAsync($"http://localhost:8000/api/user-by-token?token={token}");
            if (!response.IsSuccessStatusCode) return null;

            var user = await response.Content.ReadFromJsonAsync<UserDto>();
            return user?.id;
        }
    }

}
