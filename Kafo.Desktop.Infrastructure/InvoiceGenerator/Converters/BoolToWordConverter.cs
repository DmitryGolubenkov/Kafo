namespace Kafo.Desktop.Infrastructure.InvoiceGenerator.Converters;

public static class BoolToWordConverter
{
    public static string Convert(bool value) => value ? "Да" : "Нет";
}
