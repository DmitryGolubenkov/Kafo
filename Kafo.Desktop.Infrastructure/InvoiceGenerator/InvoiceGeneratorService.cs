using Kafo.Application.Models.Orders;
using Kafo.Desktop.Infrastructure.InvoiceGenerator.Models;
using SharpDocx;

namespace Kafo.Desktop.Infrastructure.InvoiceGenerator;

public class InvoiceGeneratorService
{
    private readonly InvoiceDocxModelFactory _factory;
    private string _templatesDirectory;
    private string _invoiceTemplateFilePath;
    private string _pathToExport;

    public InvoiceGeneratorService(InvoiceGeneratorOptions options, InvoiceDocxModelFactory factory)
    {
        
        _templatesDirectory = Path.Combine(Path.GetDirectoryName(AppContext.BaseDirectory), options.TemplatesDirectory);
        _invoiceTemplateFilePath = Path.Combine(_templatesDirectory, options.InvoiceTemplateFileName);
        _pathToExport = Path.Combine(Path.GetDirectoryName(AppContext.BaseDirectory), options.ExportPath);
        _factory = factory;
    }

    public async Task GenerateDocument(OrderModel invoice, string? exportPath = null)
    {
        await Task.Run(() => {
            var invoiceDocxModel = _factory.CreateFromInvoice(invoice);

            var document = DocumentFactory.Create(_invoiceTemplateFilePath, invoiceDocxModel);
            if(string.IsNullOrWhiteSpace(exportPath))
            {
                exportPath = _pathToExport;
            }
            if (!Directory.Exists(exportPath))
            {
                Directory.CreateDirectory(exportPath);
            }

            document.Generate(Path.Combine(exportPath, $"Выписка по заказу {invoiceDocxModel.Id}.docx"));

        }).ConfigureAwait(false);
    }
}
