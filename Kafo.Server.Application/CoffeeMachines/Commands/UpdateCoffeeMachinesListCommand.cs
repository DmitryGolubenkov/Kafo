using Kafo.Application.Interfaces;
using Kafo.Application.Models.CoffeeMachines;
using Kafo.Domain.Models;

namespace Kafo.Server.Application.CoffeeMachines.Commands;
public class UpdateCoffeeMachinesListCommand
{
    private readonly IRepository<CoffeeMachineModel> _coffeeMachines;

    public UpdateCoffeeMachinesListCommand(IRepository<CoffeeMachineModel> coffeeMachines)
    {
        _coffeeMachines = coffeeMachines;
    }

    public async Task Execute(List<CoffeeMachineModelData> input)
    {        

        foreach (var model in _coffeeMachines)
        {
            var inputModel = input.Where(x => x.Id == model.Id).FirstOrDefault();
            if (inputModel is not null)
            {
                model.ManufacturerName = inputModel.ManufacturerName;
                model.Model = inputModel.Model;
            }
            else
            {
                _coffeeMachines.Remove(model);
            }
        }

        // Новые
        var newModels = input.Where(x => x.Id is null);
        foreach(var newModel in newModels)
        {
            _coffeeMachines.Add(new CoffeeMachineModel()
            {
                ManufacturerName = newModel.ManufacturerName,
                Model = newModel.Model
            });
        }

        await _coffeeMachines.SaveChangesAsync();
    }



}
