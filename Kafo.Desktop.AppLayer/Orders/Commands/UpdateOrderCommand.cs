using Kafo.Application.Models.Orders;
using Kafo.Desktop.AppLayer.Utilities;

namespace Kafo.Desktop.AppLayer.Orders.Commands;
public class UpdateOrderCommand
{
    private readonly AuthenticatedHttpClient _httpClient;

    public UpdateOrderCommand(AuthenticatedHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task Execute(OrderModel orderModel)
    {
        await _httpClient.PostAuthenticatedAsync("api/orders/updateOrder", orderModel);
    }

}
