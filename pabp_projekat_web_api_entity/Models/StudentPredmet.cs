using System;
using System.Collections.Generic;

namespace pabp_projekat_web_api_entity.Models;

public partial class StudentPredmet
{
    public int IdStudenta { get; set; }

    public short IdPredmeta { get; set; }

    public string SkolskaGodina { get; set; } = null!;

    public virtual Predmet IdPredmetaNavigation { get; set; } = null!;

    public virtual Student IdStudentaNavigation { get; set; } = null!;
}

public partial class NewStudentPredmet
{
    public int IdStudenta { get; set; }
    public short IdPredmeta { get; set; }
    public string SkolskaGodina { get; set; } = null!;
}
