using System;
using System.Collections.Generic;

namespace pabp_projekat_web_api_entity.Models;

public partial class Ispit
{
    public int IdIspita { get; set; }

    public int IdRoka { get; set; }

    public short IdPredmeta { get; set; }

    public DateOnly Datum { get; set; }

    public virtual IspitniRok IdRokaNavigation { get; set; } = null!;

    public virtual ICollection<Zapisnik> Zapisniks { get; set; } = new List<Zapisnik>();
}
