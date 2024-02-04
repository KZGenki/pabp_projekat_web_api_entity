using System;
using System.Collections.Generic;

namespace pabp_projekat_web_api_entity.Models;

public partial class Profesor
{
    public short IdProfesora { get; set; }

    public string Ime { get; set; } = null!;

    public string Prezime { get; set; } = null!;

    public string Zvanje { get; set; } = null!;

    public DateOnly DatumZap { get; set; }

    public virtual ICollection<Predmet> Predmets { get; set; } = new List<Predmet>();
}
