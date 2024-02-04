using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace pabp_projekat_web_api_entity.Models
{
    [PrimaryKey("IdStudenta", "IdIspita")]
    public class Prijava_brojIndeksa
    {
        public int IdStudenta { get; set; }
        public int IdIspita { get; set; }

        [ForeignKey("IdIspita")]
        public virtual Ispit IdIspitaNavigation { get; set; } = null!;
        [ForeignKey("IdStudenta")]
        public virtual Student IdStudentaNavigation { get; set; } = null!;
    }
    public class PrijavaIspita
    {
        public int IdStudenta { get; set; }
        public int IdIspita { get; set; }
    }
}
