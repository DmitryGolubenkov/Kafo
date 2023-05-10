using Kafo.Application.Models.CoffeeMachines;
using Kafo.Server.Application.CoffeeMachines.Commands;
using Kafo.Server.Application.CoffeeMachines.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Kafo.Server.API.Controllers;
[Route("api/coffeeMachines")]
[ApiController]
public class CoffeeMachinesController : ControllerBase
{
    #region Fields

    private readonly GetCoffeeMachinesQuery _getCoffeeMachinesQuery;
    private readonly UpdateCoffeeMachinesListCommand _updateCoffeeMachinesListCommand;

    #endregion

    #region Constructor

    public CoffeeMachinesController(GetCoffeeMachinesQuery getCoffeeMachinesQuery, UpdateCoffeeMachinesListCommand updateCoffeeMachinesListCommand)
    {
        _getCoffeeMachinesQuery = getCoffeeMachinesQuery;
        _updateCoffeeMachinesListCommand = updateCoffeeMachinesListCommand;
    }

    #endregion

    #region Methods

    [HttpGet]
    [Route("getCoffeeMachines")]
    public async Task<List<CoffeeMachineModelData>> GetCoffeeMachines()
    {
        return await _getCoffeeMachinesQuery.Execute();
    }

    [HttpPost]
    [Route("updateCoffeeMachinesList")]
    public async Task UpdateCoffeeMachinesList(List<CoffeeMachineModelData> input)
    {
        await _updateCoffeeMachinesListCommand.Execute(input);
    }

    #endregion
}
