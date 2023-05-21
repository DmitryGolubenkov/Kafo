using Kafo.Application.Interfaces;
using Kafo.Application.Models.Orders;
using Kafo.Domain.Models;
using Kafo.Server.Application.Authorization;
using Kafo.Server.Application.Orders.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafo.Server.Application.Orders.Commands;

public class SaveNewOrderCommand
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IAccessControl _accessControl;
    private readonly IRepository<CoffeeMachineModel> _coffeeMachineModels;

    public SaveNewOrderCommand(IRepository<Order> orderRepository, IAccessControl accessControl,
        IRepository<CoffeeMachineModel> coffeeMachineModels)
    {
        _orderRepository = orderRepository;
        _accessControl = accessControl;
        _coffeeMachineModels = coffeeMachineModels;
    }

    public async Task Execute(NewOrderModel newOrderModel)
    {
        _accessControl.CheckIfAuthenticated();

        var orderMapper = new OrderMapper();

        // Создаём новый заказ на основе полученных данных
        var newOrder = orderMapper.Map(newOrderModel);

        // Создаём новый номер заказа. Он не должен совпасть с уже существующим номером 
        var random = new Random();
        do
        {
            newOrder.OrderNumber = random.Next(1000, 999999999);
        } while (_orderRepository.Any(x => x.OrderNumber == newOrder.OrderNumber));

        // Записываем нужный инстанс в качестве Foreign
        var coffeeMachine = _coffeeMachineModels.Where(x => x.Id == newOrder.CoffeeMachineId).First();
        newOrder.CoffeeMachine = coffeeMachine;

        newOrder.Id = Guid.NewGuid();
        _orderRepository.Add(newOrder);

        await _orderRepository.SaveChangesAsync();
    }
}