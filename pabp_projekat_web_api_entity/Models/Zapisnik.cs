using System;
using System.Collections.Generic;

namespace pabp_projekat_web_api_entity.Models;

public partial class Zapisnik
{
    public int IdStudenta { get; set; }

    public int IdIspita { get; set; }

    public float Ocena { get; set; }

    public string Bodovi { get; set; } = null!;

    public virtual Ispit IdIspitaNavigation { get; set; } = null!;

    public virtual Student IdStudentaNavigation { get; set; } = null!;
}
