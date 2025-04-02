using System;
using System.Collections.Generic;

namespace MapsTest.Models;

public partial class Cliente
{
    public int Idcliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Password { get; set; } = null!;
}
