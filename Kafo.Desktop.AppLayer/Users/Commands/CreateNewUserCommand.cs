using Kafo.Application.Models.Users;
using Kafo.Desktop.AppLayer.Utilities;

namespace Kafo.Desktop.AppLayer.Users.Commands;

public class CreateNewUserCommand
{
    private readonly AuthenticatedHttpClient _httpClient;

    public CreateNewUserCommand(AuthenticatedHttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task Execute(CreateNewUserInputModel model)
    {
        await _httpClient.PostAuthenticatedAsync("api/users/createNewUser", model);
    }
}