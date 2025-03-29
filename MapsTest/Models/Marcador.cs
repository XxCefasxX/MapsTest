using System;
using System.Collections.Generic;

namespace MapsTest.Models;

public partial class Marcador
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;
    public string Colonia { get; set; } = null!;
    public string Calle1 { get; set; } = null!;
    public string Calle2 { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string Latitud { get; set; } = null!;

    public string Longitud { get; set; } = null!;
    public string Categoria { get; set; } = null!;
   
}
