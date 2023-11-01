using FluentValidation;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace APICBDA.Models;

public class Sexo
{
    public Sexo()
    {
        Provas = new Collection<Prova>();
    }
    public int SexoId { get; set; }
    [Required]
    [StringLength(20)]
    public string? Genero { get; set; }
    public ICollection<Prova>? Provas { get; set; }
}

public class SexoValidator : AbstractValidator<Sexo>
{
    public SexoValidator()
    {
        RuleFor(s => s.Genero).NotNull();
        RuleFor(s => s.Genero).MaximumLength(20);
    }
}
