using System;
using System.Collections.Generic;

namespace pabp_projekat_web_api_entity.Models;

public partial class Student
{
    public int IdStudenta { get; set; }

    public string Ime { get; set; } = null!;

    public string Prezime { get; set; } = null!;

    public string Smer { get; set; } = null!;

    public short Broj { get; set; }

    public string GodinaUpisa { get; set; } = null!;

    public virtual ICollection<StudentPredmet> StudentPredmets { get; set; } = new List<StudentPredmet>();

    public virtual ICollection<Zapisnik> Zapisniks { get; set; } = new List<Zapisnik>();

    public virtual ICollection<Prijava_brojIndeksa> Prijava_BrojIndeksas { get; set; } = new List<Prijava_brojIndeksa>();
}
