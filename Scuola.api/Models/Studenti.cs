using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Scuola.api.Models;

[Table("Studenti")]
[Index("Email", Name = "UQ__Studenti__A9D105343DFA10DE", IsUnique = true)]
public partial class Studenti
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Nome { get; set; } = null!;

    [StringLength(50)]
    public string Cognome { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    public DateOnly? DataNascita { get; set; }

    [StringLength(10)]
    public string? Classe { get; set; }

    [InverseProperty("Studente")]
    public virtual ICollection<Iscrizioni> Iscrizionis { get; set; } = new List<Iscrizioni>();

    [InverseProperty("Studente")]
    public virtual ICollection<Voti> Votis { get; set; } = new List<Voti>();
}
