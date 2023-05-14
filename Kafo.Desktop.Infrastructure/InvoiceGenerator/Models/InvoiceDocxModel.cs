namespace Kafo.Desktop.Infrastructure.InvoiceGenerator.Models;

#nullable disable
#pragma warning disable CS8632
public class InvoiceDocxModel
{
    internal InvoiceDocxModel(){}

    public string Id { get; set; }
    public string ClientName { get; set; }
    public string EmployeePhoneNumber { get; set; }
    public string ClientPhonePrimary { get; set; }
    public string? ClientPhoneSecondary { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public string Appearance { get; set; }
    public string Malfunction { get; set; }
    public string AcceptanceDate { get; set; }

    // Комплектация
    public string Pallet { get; set; }
    public string Filter { get; set; }
    public string WaterTank { get; set; }
    public string CoffeeLid { get; set; }
    public string WasteTray { get; set; }
    public string CappuccinatorHose { get; set; }
    public string PowerCord { get; set; }
    public string MilkKettle { get; set; }
    public string HotWaterNozzle { get; set; }
    public string CappuccinatorNozzle { get; set; }
    public string? OtherText { get; set; }
}
