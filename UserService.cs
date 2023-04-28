using System.Text.Json;
using Microsoft.Extensions.Logging;

public class UserService : IUserService {

  private readonly IHttpClientFactory _httpClientFactory = null!;
  private readonly ILogger<UserService> _logger = null!;

  public UserService(IHttpClientFactory httpClientFactory, ILogger<UserService> logger) =>
        (_httpClientFactory, _logger) = (httpClientFactory, logger);

  public async Task<User[]> GetUsersAsync() {
        _logger.LogInformation("Executing GetUsersAsync");

        // Create the client
        using HttpClient client = _httpClientFactory.CreateClient();

        try
        {
            // Make HTTP GET request
            // Parse JSON response deserialize into Todo types
            User[]? users = await client.GetFromJsonAsync<User[]>(
                $"https://jsonplaceholder.typicode.com/users",
                new JsonSerializerOptions(JsonSerializerDefaults.Web));

            return users ?? Array.Empty<User>();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error getting something fun to say: {Error}", ex);
        }

        return Array.Empty<User>();
  }
}
