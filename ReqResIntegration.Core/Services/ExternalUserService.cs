using ReqResIntegration.Core.Models;
using ReqResIntegration.Core.Interfaces;
using System.Net.Http.Json;

namespace ReqResIntegration.Core.Services;

public class ExternalUserService : IExternalUserService
{
    private readonly HttpClient _httpClient;

    public ExternalUserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserDto> GetUserByIdAsync(int userId)
    {
        var response = await _httpClient.GetFromJsonAsync<Dictionary<string, UserDto>>($"users/{userId}");
        return response?["data"];
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = new List<UserDto>();
        int page = 1;
        int totalPages;

        do
        {
            var response = await _httpClient.GetFromJsonAsync<UserListResponse>($"users?page={page}");
            if (response?.Data != null)
            {
                users.AddRange(response.Data);
                totalPages = response.Total_Pages;
            }
            else
            {
                break;
            }
            page++;
        } while (page <= totalPages);

        return users;
    }
}
