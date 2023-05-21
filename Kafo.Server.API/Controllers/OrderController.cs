using Kafo.Application.Models.Orders;
using Kafo.Server.Application.Orders.Commands;
using Kafo.Server.Application.Orders.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Kafo.Server.API.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly GetAllOrdersQuery _getAllOrdersQuery;
    private readonly SaveNewOrderCommand _saveNewOrderCommand;
    private readonly UpdateOrderCommand _updateOrderCommand;
    private readonly DeleteOrderCommand _deleteOrderCommand;

    public OrderController(GetAllOrdersQuery getAllOrdersQuery, SaveNewOrderCommand saveNewOrderCommand, UpdateOrderCommand updateOrderCommand, DeleteOrderCommand deleteOrderCommand)
    {
        _getAllOrdersQuery = getAllOrdersQuery;
        _saveNewOrderCommand = saveNewOrderCommand;
        _updateOrderCommand = updateOrderCommand;
        _deleteOrderCommand = deleteOrderCommand;
    }

    [HttpGet]
    [Route("getAllOrders")]
    public async Task<List<OrderModel>> GetAllOrders()
    {
        return await _getAllOrdersQuery.Execute();
    }


    [HttpPost]
    [Route("createNewOrder")]
    public async Task SaveNewOrder(NewOrderModel newOrderModel)
    {
        await _saveNewOrderCommand.Execute(newOrderModel);
    }

    [HttpPost]
    [Route("updateOrder")]
    public async Task UpdateOrder(OrderModel orderModel)
    {
        await _updateOrderCommand.Execute(orderModel);
    }

    [HttpPost]
    [Route("deleteOrder")]
    public async Task DeleteOrder(DeleteOrderModel orderModel)
    {
        await _deleteOrderCommand.Execute(orderModel.Id);
    }

}
