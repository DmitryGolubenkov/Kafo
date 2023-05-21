using Kafo.Application.Models.CoffeeMachines;
using Kafo.Domain.Dictionaries;
using Kafo.Domain.Models;

namespace Kafo.Application.Models.Orders;
public class NewOrderModel
{
    // ФИО клиента
    public string ClientName { get; set; }

    /// <summary>
    /// Номер телефона сотрудника
    /// </summary>
    public string EmployeePhoneNumber { get; set; }

    /// <summary>
    /// Номер телефона клиента
    /// </summary>
    public string ClientPhonePrimary { get; set; }

    /// <summary>
    /// Дополнительный номер телефона клиента
    /// </summary>
    public string? ClientPhoneSecondary { get; set; }

    /// <summary>
    /// Серийный номер кофемашины
    /// </summary>
    public string SerialNumber { get; set; }

    /// <summary>
    /// Ремонтируемая модель кофемашины
    /// </summary>
    public CoffeeMachineModelData CoffeeMachine { get; set; }

    /// <summary>
    /// Внешний вид
    /// </summary>
    public string Appearance { get; set; } = "б/у";

    /// <summary>
    /// Заявленная неисправность
    /// </summary>
    public string? Malfunction { get; set; }

    /// <summary>
    /// Время приёмки
    /// </summary>
    public DateTime AcceptanceDate { get; set; }

    // Комплектация
    public bool Pallet { get; set; }
    public bool Filter { get; set; }
    public bool WaterTank { get; set; }
    public bool CoffeeLid { get; set; }
    public bool WasteTray { get; set; }
    public bool CappuccinatorHose { get; set; }
    public bool PowerCord { get; set; }
    public bool MilkKettle { get; set; }
    public bool HotWaterNozzle { get; set; }
    public bool CappuccinatorNozzle { get; set; }
    public string? OtherText { get; set; }

}
