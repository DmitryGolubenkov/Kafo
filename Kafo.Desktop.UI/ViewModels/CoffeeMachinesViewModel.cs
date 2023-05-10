using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kafo.Application.Models.CoffeeMachines;
using Kafo.Desktop.AppLayer.CoffeeMachines.Commands;
using Kafo.Desktop.AppLayer.CoffeeMachines.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kafo.Desktop.UI.ViewModels;
public partial class CoffeeMachinesViewModel : ObservableObject
{
    #region Properties

    [ObservableProperty]
    private List<CoffeeMachineModelData> coffeeMachines;


    #endregion

    #region Fields

    private readonly GetCoffeeMachinesQuery _getCoffeeMachinesQuery;
    private readonly UpdateCoffeeMachinesListCommand _updateCoffeeMachinesListCommand;

    #endregion

    public CoffeeMachinesViewModel(GetCoffeeMachinesQuery getCoffeeMachinesQuery, UpdateCoffeeMachinesListCommand updateCoffeeMachinesListCommand)
    {
        _getCoffeeMachinesQuery = getCoffeeMachinesQuery;
        _updateCoffeeMachinesListCommand = updateCoffeeMachinesListCommand;
    }
    private async Task RefreshCoffeeMachinesList()
    {
        CoffeeMachines = (await _getCoffeeMachinesQuery.Execute());
    }

    /// <summary>
    /// Вызывается при загрузке UI элемента
    /// </summary>
    [RelayCommand]
    public async Task ControlLoaded()
    {
        await RefreshCoffeeMachinesList();
    }

    [RelayCommand]
    public async Task SaveChangesInCoffeeMachines()
    {
        await _updateCoffeeMachinesListCommand.Execute(CoffeeMachines);
    }

}
