using Kafo.Application.Interfaces;
using Kafo.Application.Models.Orders;
using Kafo.Domain.Models;
using Kafo.Server.Application.Authorization;
using Kafo.Server.Application.Orders.Mappers;

namespace Kafo.Server.Application.Orders.Queries;
public class GetAllOrdersQuery
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IAccessControl _accessControl;

    public GetAllOrdersQuery(IRepository<Order> orderRepository, IAccessControl accessControl)
    {
        _orderRepository = orderRepository;
        _accessControl = accessControl;
    }


    public async Task<List<OrderModel>> Execute()
    {
        _accessControl.CheckIfAuthenticated();

        return _orderRepository.ProjectToModel().ToList();

        /*return _orderRepository.Select(x => new OrderModel()
        {
            Id = x.Id,
            ClientName = x.ClientName,
            AcceptanceDate = x.AcceptanceDate,
            CappuccinatorHose = x.CappuccinatorHose,
            CappuccinatorNozzle = x.CappuccinatorNozzle,
            ClientPhonePrimary = x.ClientPhonePrimary,
            CoffeeLid = x.CoffeeLid,
            CoffeeMachine = x.CoffeeMachine,
            EmployeePhoneNumber = x.EmployeePhoneNumber,
            Filter = x.Filter,
            HotWaterNozzle = x.HotWaterNozzle,
            MilkKettle = x.MilkKettle,
            OrderNumber = x.OrderNumber,
            Pallet = x.Pallet,
            PowerCord = x.PowerCord,
            SerialNumber = x.SerialNumber,
            WasteTray = x.WasteTray,
            WaterTank = x.WaterTank,
            Appearance = x.Appearance,
            ClientPhoneSecondary = x.ClientPhoneSecondary,
            Malfunction = x.Malfunction,
            OrderFinishDate = x.OrderFinishDate,
            OtherText = x.OtherText,
            WarrantyBefore = x.WarrantyBefore
        }).ToList();*/
    }
}
