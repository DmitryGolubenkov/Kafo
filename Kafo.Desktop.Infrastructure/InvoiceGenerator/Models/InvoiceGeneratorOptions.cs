namespace Kafo.Desktop.Infrastructure.InvoiceGenerator.Models;

public class InvoiceGeneratorOptions
{
    public string TemplatesDirectory { get; set; }
    public string InvoiceTemplateFileName{ get; set; }
    public string ExportPath { get; set; }
}