﻿namespace Kafo.Domain.Models;

public class CoffeeMachineModel : EntityBase
{
    public string ManufacturerName { get; set; }
    public string Model { get; set; }
    public ICollection<Order> Orders { get; set; }
}