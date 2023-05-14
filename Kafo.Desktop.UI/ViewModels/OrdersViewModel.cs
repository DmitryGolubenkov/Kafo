using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kafo.Application.Models.CoffeeMachines;
using Kafo.Application.Models.Orders;
using Kafo.Desktop.AppLayer.CoffeeMachines.Queries;
using Kafo.Desktop.AppLayer.Orders.Commands;
using Kafo.Desktop.AppLayer.Orders.Queries;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using Kafo.Desktop.AppLayer.Orders.Events;
using Kafo.Desktop.Infrastructure.InvoiceGenerator;
using Kafo.Desktop.Infrastructure.InvoiceGenerator.Models;

namespace Kafo.Desktop.UI.ViewModels;
public partial class OrdersViewModel : ObservableObject
{
    #region Constructor

    public OrdersViewModel(GetAllOrdersQuery getAllOrdersQuery, UpdateOrderCommand updateOrderCommand, GetCoffeeMachinesQuery getCoffeeMachinesQuery, GetPhoneNumbersQuery getPhoneNumbersQuery, IMessenger messenger, InvoiceGeneratorService invoiceGeneratorService, InvoiceGeneratorOptions invoiceGeneratorOptions)
    {
        _getAllOrdersQuery = getAllOrdersQuery;
        _updateOrderCommand = updateOrderCommand;
        _getCoffeeMachinesQuery = getCoffeeMachinesQuery;
        _getPhoneNumbersQuery = getPhoneNumbersQuery;
        _messenger = messenger;
        _invoiceGeneratorService = invoiceGeneratorService;
        _invoiceGeneratorOptions = invoiceGeneratorOptions;

        _messenger.Register<NewOrderCreatedEvent>(this, (recipient, message) => LoadData().Wait());
    }

    #endregion

    #region Fields

    private readonly GetAllOrdersQuery _getAllOrdersQuery;
    private readonly UpdateOrderCommand _updateOrderCommand;
    private readonly GetCoffeeMachinesQuery _getCoffeeMachinesQuery;
    private readonly GetPhoneNumbersQuery _getPhoneNumbersQuery;
    private readonly IMessenger _messenger;
    private readonly InvoiceGeneratorService _invoiceGeneratorService;
    private readonly InvoiceGeneratorOptions _invoiceGeneratorOptions;
    
    #endregion

    #region ObservableProperties

    [ObservableProperty]
    private List<OrderModel> _orders;
    [ObservableProperty]
    private OrderModel? _selectedOrder;
    partial void OnSelectedOrderChanged(OrderModel? value)
    {
        OrderSelected = SelectedOrder is not null;
    }
    
    [ObservableProperty]
    private bool _orderSelected;
    [ObservableProperty]
    private List<CoffeeMachineModelData> _coffeeMachines;
    [ObservableProperty]
    private List<string> _phoneNumbers;

    #endregion

    #region Commands

    [RelayCommand]
    public async Task LoadData()
    {
        Orders = await _getAllOrdersQuery.Execute();
        CoffeeMachines = await _getCoffeeMachinesQuery.Execute();
        PhoneNumbers = await _getPhoneNumbersQuery.Execute();

        foreach (var order in Orders)
        {
            order.CoffeeMachine = CoffeeMachines.Where(x => order.CoffeeMachine.Id == x.Id).First();
        }
    }

    [RelayCommand]
    public async Task ControlLoaded()
    {
        await LoadData();
    }

    [RelayCommand]
    public async Task UpdateOrder()
    {


        await _updateOrderCommand.Execute(SelectedOrder);
    }

    [RelayCommand]
    public async Task ExportOrder()
    {
        await _invoiceGeneratorService.GenerateDocument(SelectedOrder, null);
    }

    #endregion
    
    [RelayCommand]
    private void OpenExportFolder()
    {
        try
        {
            var path = Path.Combine(Path.GetDirectoryName(AppContext.BaseDirectory), _invoiceGeneratorOptions.ExportPath);
            if (Directory.Exists(path))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = path,
                    FileName = "explorer.exe"
                };

                Process.Start(startInfo);
            }
            else
            {
                MessageBox.Show(string.Format("Папка не существует."), "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка открытия папки: {ex.Message}.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

}
