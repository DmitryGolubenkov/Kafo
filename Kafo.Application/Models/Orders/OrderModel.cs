
using Kafo.Application.Models.CoffeeMachines;
using Kafo.Domain.Dictionaries;
using Kafo.Domain.Models;

namespace Kafo.Application.Models.Orders;
public class OrderModel
{
    /// <summary>
    /// ID заказа
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Номер заказа
    /// </summary>
    public required int OrderNumber { get; set; }

    // ФИО клиента
    public required string ClientName { get; set; }

    /// <summary>
    /// Номер телефона сотрудника
    /// </summary>
    public required string EmployeePhoneNumber { get; set; }

    /// <summary>
    /// Номер телефона клиента
    /// </summary>
    public required string ClientPhonePrimary { get; set; }

    /// <summary>
    /// Дополнительный номер телефона клиента
    /// </summary>
    public string? ClientPhoneSecondary { get; set; }

    /// <summary>
    /// Серийный номер кофемашины
    /// </summary>
    public required string SerialNumber { get; set; }

    /// <summary>
    /// Ремонтируемая модель кофемашины
    /// </summary>
    public required CoffeeMachineModelData CoffeeMachine { get; set; }

    /// <summary>
    /// Внешний вид
    /// </summary>
    public string Appearance { get; set; }

    /// <summary>
    /// Заявленная неисправность
    /// </summary>
    public string? Malfunction { get; set; }

    /// <summary>
    /// Время приёмки
    /// </summary>
    public DateTime AcceptanceDate { get; set; }

    /// <summary>
    /// Время завершения заказа
    /// </summary>
    public DateTime OrderFinishDate { get; set; }

    /// <summary>
    /// До какой даты действует гарантия на ремонт
    /// </summary>
    public DateTime? WarrantyBefore { get; set; }

    // Комплектация
    public required bool Pallet { get; set; }
    public required bool Filter { get; set; }
    public required bool WaterTank { get; set; }
    public required bool CoffeeLid { get; set; }
    public required bool WasteTray { get; set; }
    public required bool CappuccinatorHose { get; set; }
    public required bool PowerCord { get; set; }
    public required bool MilkKettle { get; set; }
    public required bool HotWaterNozzle { get; set; }
    public required bool CappuccinatorNozzle { get; set; }
    public string? OtherText { get; set; }
}
