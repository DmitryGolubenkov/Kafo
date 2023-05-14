using Kafo.Application.Models.Orders;
using Kafo.Desktop.Infrastructure.InvoiceGenerator.Converters;
using Kafo.Desktop.Infrastructure.InvoiceGenerator.Models;

namespace Kafo.Desktop.Infrastructure.InvoiceGenerator;

public class InvoiceDocxModelFactory
{
    public InvoiceDocxModel CreateFromInvoice(OrderModel invoice)
    {
        return new InvoiceDocxModel
        {
            Id =  invoice.OrderNumber.ToString(),
            ClientName = invoice.ClientName,
            EmployeePhoneNumber = invoice.EmployeePhoneNumber,
            ClientPhonePrimary = invoice.ClientPhonePrimary,
            ClientPhoneSecondary = invoice.ClientPhoneSecondary,
            Manufacturer = invoice.CoffeeMachine.ManufacturerName,
            Model = invoice.CoffeeMachine.Model,
            Appearance = invoice.Appearance,
            Malfunction = invoice.Malfunction,
            AcceptanceDate = invoice.AcceptanceDate.ToString("dd.MM.yyyy"),
            Pallet = BoolToWordConverter.Convert(invoice.Pallet),
            Filter = BoolToWordConverter.Convert(invoice.Pallet),
            WaterTank = BoolToWordConverter.Convert(invoice.WaterTank),
            CoffeeLid = BoolToWordConverter.Convert(invoice.CoffeeLid),
            WasteTray = BoolToWordConverter.Convert(invoice.WasteTray),
            CappuccinatorHose = BoolToWordConverter.Convert(invoice.CappuccinatorHose),
            PowerCord = BoolToWordConverter.Convert(invoice.PowerCord),
            MilkKettle = BoolToWordConverter.Convert(invoice.MilkKettle),
            HotWaterNozzle = BoolToWordConverter.Convert(invoice.HotWaterNozzle),
            CappuccinatorNozzle = BoolToWordConverter.Convert(invoice.CappuccinatorNozzle),
            OtherText = !string.IsNullOrWhiteSpace(invoice.OtherText) ? invoice.OtherText : "Отсутствует"
        };
    }
}
