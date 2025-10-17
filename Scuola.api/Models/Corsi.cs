using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Scuola.api.Models;

[Table("Corsi")]
public partial class Corsi
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string NomeCorso { get; set; } = null!;

    [StringLength(255)]
    public string? Descrizione { get; set; }

    public int? DocenteId { get; set; }

    [ForeignKey("DocenteId")]
    [InverseProperty("Corsis")]
    public virtual Docenti? Docente { get; set; }

    [InverseProperty("Corso")]
    public virtual ICollection<Iscrizioni> Iscrizionis { get; set; } = new List<Iscrizioni>();

    [InverseProperty("Corso")]
    public virtual ICollection<Voti> Votis { get; set; } = new List<Voti>();
}
