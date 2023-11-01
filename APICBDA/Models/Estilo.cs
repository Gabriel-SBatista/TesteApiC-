using FluentValidation;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace APICBDA.Models;

public class Estilo
{
    public Estilo()
    {
        Provas = new Collection<Prova>();
    }
    public int EstiloId { get; set; }
    [Required]
    [MaxLength(20)]
    public string? Nome { get; set; }
    public ICollection<Prova>? Provas { get; set; }
}

public class EstiloValidator : AbstractValidator<Estilo>
{
    public EstiloValidator()
    {
        RuleFor(e => e.Nome).NotNull();
        RuleFor(e => e.Nome).MaximumLength(20);
    }
}