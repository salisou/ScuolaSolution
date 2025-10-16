using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Scuola.api.Models;

[Table("Iscrizioni")]
[Index("StudenteId", "CorsoId", Name = "UQ_Iscrizioni", IsUnique = true)]
public partial class Iscrizioni
{
    [Key]
    public int Id { get; set; }

    public int StudenteId { get; set; }

    public int CorsoId { get; set; }

    public DateOnly DataIscrizione { get; set; }

    [ForeignKey("CorsoId")]
    [InverseProperty("Iscrizionis")]
    public virtual Corsi Corso { get; set; } = null!;

    [ForeignKey("StudenteId")]
    [InverseProperty("Iscrizionis")]
    public virtual Studenti Studente { get; set; } = null!;
}
