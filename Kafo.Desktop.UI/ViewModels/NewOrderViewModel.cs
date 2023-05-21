using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kafo.Application.Models.CoffeeMachines;
using Kafo.Application.Models.Orders;
using Kafo.Desktop.AppLayer.Authorization;
using Kafo.Desktop.AppLayer.CoffeeMachines.Queries;
using Kafo.Desktop.AppLayer.Orders.Commands;
using Kafo.Desktop.AppLayer.Orders.Queries;
using System.Linq;
using CommunityToolkit.Mvvm.Messaging;
using Kafo.Desktop.AppLayer.Orders.Events;

namespace Kafo.Desktop.UI.ViewModels;

public partial class NewOrderViewModel : ObservableObject
{
    private readonly CreateNewOrderCommand _createNewOrderCommand;
    private readonly UserInfo _userInfo;
    private readonly GetPhoneNumbersQuery _getPhoneNumbersQuery;
    private readonly GetCoffeeMachinesQuery _getCoffeeMachinesQuery;
    private readonly IMessenger _messenger;

    [ObservableProperty] private NewOrderModel _newOrder = new NewOrderModel();
    [ObservableProperty] private List<CoffeeMachineModelData> _coffeeMachines;
    [ObservableProperty] private List<string> _phoneNumbers;

    public NewOrderViewModel(CreateNewOrderCommand createNewOrderCommand, UserInfo userInfo,
        GetCoffeeMachinesQuery getCoffeeMachinesQuery, GetPhoneNumbersQuery getPhoneNumbersQuery, IMessenger messenger)
    {
        _createNewOrderCommand = createNewOrderCommand;
        _userInfo = userInfo;
        _getCoffeeMachinesQuery = getCoffeeMachinesQuery;
        _getPhoneNumbersQuery = getPhoneNumbersQuery;
        _messenger = messenger;
    }

    [RelayCommand]
    public async Task LoadData()
    {
        CoffeeMachines = await _getCoffeeMachinesQuery.Execute();
        PhoneNumbers = await _getPhoneNumbersQuery.Execute();
        NewOrder.EmployeePhoneNumber = PhoneNumbers.Where(x => x == _userInfo.PhoneNumber).FirstOrDefault();
        NewOrder.AcceptanceDate = DateTime.Now;
        OnPropertyChanged(nameof(NewOrder));
    }

    [RelayCommand]
    public async Task ControlLoaded()
    {
        await LoadData();
    }


    [RelayCommand]
    public async Task CreateNewOrder()
    {
        await _createNewOrderCommand.Execute(NewOrder);

        await Task.Run(() => _messenger.Send<NewOrderCreatedEvent>());
    }
}