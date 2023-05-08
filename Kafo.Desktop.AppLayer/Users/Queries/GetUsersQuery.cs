using Kafo.Application.Models.Users;
using Kafo.Desktop.AppLayer.Utilities;

namespace Kafo.Desktop.AppLayer.Users.Queries;
public class GetUsersQuery
{
    private readonly AuthenticatedHttpClient httpClient;

    public GetUsersQuery(AuthenticatedHttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<List<UserModel>> Execute()
    {
        return await httpClient.GetAuthenticatedAsync<List<UserModel>>("api/users/getUsers");
    }
}
