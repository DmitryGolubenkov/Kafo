using Kafo.Application.Interfaces;
using Kafo.Application.Models.CoffeeMachines;
using Kafo.Domain.Models;
using Kafo.Server.Application.Authorization;

namespace Kafo.Server.Application.CoffeeMachines.Queries;

/// <summary>
/// Возвращает все модели кофемашин, сохранённые в системе
/// </summary>
public class GetCoffeeMachinesQuery
{
    private readonly IRepository<CoffeeMachineModel> _coffeeMachinesRepository;
    private readonly IAccessControl _accessControl;

    public GetCoffeeMachinesQuery(IRepository<CoffeeMachineModel> coffeeMachinesRepository, IAccessControl accessControl)
    {
        _coffeeMachinesRepository = coffeeMachinesRepository;
        _accessControl = accessControl;
    }

    /// <summary>
    /// Возвращает все модели кофемашин, сохранённые в системе
    /// </summary>
    public async Task<List<CoffeeMachineModelData>> Execute()
    {
        _accessControl.CheckIfAuthenticated();

        var data = _coffeeMachinesRepository.Select(model => new CoffeeMachineModelData()
        {
            Id = model.Id,
            ManufacturerName = model.ManufacturerName,
            Model = model.Model
        });

        if (data.Any())
            return data.ToList();
        else 
            return new List<CoffeeMachineModelData>();
    }
}
