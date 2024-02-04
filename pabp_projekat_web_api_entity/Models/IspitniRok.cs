using System;
using System.Collections.Generic;

namespace pabp_projekat_web_api_entity.Models;

public partial class IspitniRok
{
    public int IdRoka { get; set; }

    public string Naziv { get; set; } = null!;

    public string SkolskaGod { get; set; } = null!;

    public string StatusRoka { get; set; } = null!;

    public virtual ICollection<Ispit> Ispits { get; set; } = new List<Ispit>();
}
