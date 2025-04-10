using System;
using System.Collections.Generic;

namespace ApiVehiculos.Models;

public partial class Usuario
{
    public int IdUser { get; set; }

    public string? NombreUser { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }
}
