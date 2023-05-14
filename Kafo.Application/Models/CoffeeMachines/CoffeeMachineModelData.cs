namespace Kafo.Application.Models.CoffeeMachines;
public class CoffeeMachineModelData
{
    public CoffeeMachineModelData()
    {
        // Нужен для таблицы почему-то
    }
    public Guid? Id { get; set; }
    public string ManufacturerName { get; set; }
    public string Model { get; set; }

    public override string ToString()
    {
        return $"{ManufacturerName} {Model}";
    }
}
