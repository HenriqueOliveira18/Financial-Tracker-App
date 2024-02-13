using System.Net.Http.Json;
using Tracker.Application.Interfaces;
using Tracker.Domain.Entities;

namespace Tracker.Application.Services.Clients
{
    public class AccountClient : IDbClient<Account>
    {
        private readonly HttpClient _httpClient;

        public AccountClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Account> AddAsync(Account entity)
        {
            var response = await _httpClient.PostAsJsonAsync("accounts", entity);

            if (response.IsSuccessStatusCode)
            {
                // If the request was successful, deserialize the JSON content to Account
                var createdAccount = await response.Content.ReadFromJsonAsync<Account>();
                return createdAccount;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}, Reason: {response.ReasonPhrase}, Content: {errorContent}");
            }
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("accounts");

            if(response.IsSuccessStatusCode)
            {
                var accounts = await response.Content.ReadFromJsonAsync<IEnumerable<Account>>();
                return accounts.ToList() ?? new List<Account>();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}, Error: {errorContent}");
            }
        }

        public async Task<Account?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"accounts/{id}");

            if (response.IsSuccessStatusCode)
            {
                var accounts = await response.Content.ReadFromJsonAsync<Account>();
                return accounts;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Handle not found (404) returning null.
                return default(Account); 
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}, Error: {errorContent}");
            }
        }
        
        public async Task<Account?> UpdateAsync(Account entity)
        {
            var response = await _httpClient.PutAsJsonAsync($"accounts/{entity.Id}", entity);

            if (response.IsSuccessStatusCode)
            {
                var updatedAccount = await response.Content.ReadFromJsonAsync<Account>();
                return updatedAccount;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return default(Account); // Handle not found (404) returning default value.
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}, Error: {errorContent}");
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"accounts/{id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}, Error: {errorContent}");
            }
        }
    }
}
