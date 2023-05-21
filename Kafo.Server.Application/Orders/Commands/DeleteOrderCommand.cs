using Kafo.Application.Interfaces;
using Kafo.Application.Models.Orders;
using Kafo.Domain.Models;
using Kafo.Server.Application.Authorization;

namespace Kafo.Server.Application.Orders.Commands;

public class DeleteOrderCommand
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IAccessControl _accessControl;

    public DeleteOrderCommand(IRepository<Order> orderRepository, IAccessControl accessControl)
    {
        _orderRepository = orderRepository;
        _accessControl = accessControl;
    }

    public async Task Execute(Guid orderId)
    {
        _accessControl.CheckIfAuthenticated();

        var order = _orderRepository.Where(x => x.Id == orderId).FirstOrDefault();

        if (order is null)
            return;

        _orderRepository.Remove(order);

        await _orderRepository.SaveChangesAsync();
    }
}