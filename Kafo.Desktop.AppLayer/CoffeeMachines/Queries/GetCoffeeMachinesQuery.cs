using Kafo.Application.Models.CoffeeMachines;
using Kafo.Desktop.AppLayer.Utilities;

namespace Kafo.Desktop.AppLayer.CoffeeMachines.Queries;
public class GetCoffeeMachinesQuery
{
    #region Fields

    private readonly AuthenticatedHttpClient httpClient;

    #endregion

    #region Constructor

    public GetCoffeeMachinesQuery(AuthenticatedHttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    #endregion

    #region Methods

    public async Task<List<CoffeeMachineModelData>> Execute()
    {
        return await httpClient.GetAuthenticatedAsync<List<CoffeeMachineModelData>>("api/coffeeMachines/getCoffeeMachines");
    }

    #endregion
}
