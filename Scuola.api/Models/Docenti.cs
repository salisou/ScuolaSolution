using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Scuola.api.Models;

[Table("Docenti")]
[Index("Email", Name = "UQ__Docenti__A9D10534C56CBE05", IsUnique = true)]
public partial class Docenti
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Nome { get; set; } = null!;

    [StringLength(50)]
    public string Cognome { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [StringLength(100)]
    public string? MateriaPrincipale { get; set; }

    [InverseProperty("Docente")]
    public virtual ICollection<Corsi> Corsis { get; set; } = new List<Corsi>();
}
