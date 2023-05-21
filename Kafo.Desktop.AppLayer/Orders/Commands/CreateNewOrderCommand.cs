using Kafo.Application.Models.Orders;
using Kafo.Desktop.AppLayer.Utilities;

namespace Kafo.Desktop.AppLayer.Orders.Commands;
public class CreateNewOrderCommand
{
    private readonly AuthenticatedHttpClient _httpClient;

    public CreateNewOrderCommand(AuthenticatedHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task Execute(NewOrderModel newOrderModel)
    {
        await _httpClient.PostAuthenticatedAsync("api/orders/createNewOrder", newOrderModel);
    }
}