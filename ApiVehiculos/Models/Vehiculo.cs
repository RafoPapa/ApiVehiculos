using System;
using System.Collections.Generic;

namespace ApiVehiculos.Models;

public partial class Vehiculo
{
    public int Id { get; set; }

    public string? PlacaVeh { get; set; }

    public string? Marca { get; set; }

    public string? DescripVehiculo { get; set; }

    public int? AnioFabVeh { get; set; }

    public string? PropietarioVeh { get; set; }

    public string? ImagenVeh { get; set; }
}
