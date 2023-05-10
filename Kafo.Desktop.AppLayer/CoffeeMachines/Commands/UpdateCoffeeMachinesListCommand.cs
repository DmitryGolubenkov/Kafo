using Kafo.Application.Models.CoffeeMachines;
using Kafo.Desktop.AppLayer.Utilities;

namespace Kafo.Desktop.AppLayer.CoffeeMachines.Commands;
public class UpdateCoffeeMachinesListCommand
{

    private readonly AuthenticatedHttpClient _httpClient;


    public UpdateCoffeeMachinesListCommand(AuthenticatedHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task Execute(List<CoffeeMachineModelData> model)
    {
        await _httpClient.PostAuthenticatedAsync("api/coffeeMachines/updateCoffeeMachinesList", model);
    }
}
