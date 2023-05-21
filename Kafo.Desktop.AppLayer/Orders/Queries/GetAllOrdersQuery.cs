using Kafo.Application.Models.Orders;
using Kafo.Desktop.AppLayer.Utilities;

namespace Kafo.Desktop.AppLayer.Orders.Queries;
public class GetAllOrdersQuery
{
    #region Fields

    private readonly AuthenticatedHttpClient httpClient;

    #endregion

    #region Constructor

    public GetAllOrdersQuery(AuthenticatedHttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    #endregion

    #region Methods

    public async Task<List<OrderModel>> Execute()
    {
        return await httpClient.GetAuthenticatedAsync<List<OrderModel>>("api/orders/getAllOrders");
    }

    #endregion

}
