using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Scuola.api.Models;

[Table("Voti")]
public partial class Voti
{
    [Key]
    public int Id { get; set; }

    public int StudenteId { get; set; }

    public int CorsoId { get; set; }

    [Column(TypeName = "decimal(4, 2)")]
    public decimal Valutazione { get; set; }

    public DateOnly DataVoto { get; set; }

    [ForeignKey("CorsoId")]
    [InverseProperty("Votis")]
    public virtual Corsi Corso { get; set; } = null!;

    [ForeignKey("StudenteId")]
    [InverseProperty("Votis")]
    public virtual Studenti Studente { get; set; } = null!;
}
