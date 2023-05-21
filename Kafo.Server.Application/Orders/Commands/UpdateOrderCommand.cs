using Kafo.Application.Interfaces;
using Kafo.Application.Models.Orders;
using Kafo.Domain.Models;
using Kafo.Server.Application.Authorization;

namespace Kafo.Server.Application.Orders.Commands;
public class UpdateOrderCommand
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IAccessControl _accessControl;
    private readonly IRepository<CoffeeMachineModel> _coffeeMachineModels;

    public UpdateOrderCommand(IRepository<Order> orderRepository, IAccessControl accessControl, IRepository<CoffeeMachineModel> coffeeMachineModels)
    {
        _orderRepository = orderRepository;
        _accessControl = accessControl;
        _coffeeMachineModels = coffeeMachineModels;
    }

    public async Task Execute(OrderModel orderModel)
    {
        _accessControl.CheckIfAuthenticated();

        var order = _orderRepository.Where(x => x.Id == orderModel.Id).FirstOrDefault();

        if (order is null)
            throw new Exception("Заказ не существует");

        foreach(var property in orderModel.GetType().GetProperties())
        {
            if(property.Name==nameof(orderModel.CoffeeMachine))
                continue;
            order.GetType().GetProperty(property.Name).SetValue(order, property.GetValue(orderModel));
        }

        // Записываем нужный инстанс в качестве Foreign
        var coffeeMachine = _coffeeMachineModels.Where(x => x.Id == orderModel.CoffeeMachine.Id).First();
        order.CoffeeMachine = coffeeMachine;

        //order = updatedOrder;
        await _orderRepository.SaveChangesAsync();
    }
}
