using Kafo.Application.Models.Orders;
using Kafo.Desktop.AppLayer.Utilities;

namespace Kafo.Desktop.AppLayer.Orders.Commands;

public class DeleteOrderCommand
{
    private readonly AuthenticatedHttpClient _httpClient;

    public DeleteOrderCommand(AuthenticatedHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task Execute(DeleteOrderModel deleteOrderModel)
    {
        await _httpClient.PostAuthenticatedAsync("api/orders/deleteOrder", deleteOrderModel);
    }

}
