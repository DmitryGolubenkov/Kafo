using Kafo.Application.Models.Orders;
using Kafo.Domain.Models;
using Riok.Mapperly.Abstractions;

namespace Kafo.Server.Application.Orders.Mappers;

[Mapper]
public partial class OrderMapper
{
    public partial Order Map(NewOrderModel newOrderModel);
    public partial Order Map(OrderModel orderModel);
    public partial OrderModel Map(Order order);
}

[Mapper]
public static partial class OrderStaticMapper
{
    public static partial IQueryable<OrderModel> ProjectToModel(this IQueryable<Order> q);
}