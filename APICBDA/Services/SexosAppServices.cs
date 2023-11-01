using APICBDA.Context;
using APICBDA.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace APICBDA.Services;

public class SexosAppServices
{
    private readonly AppDbContext _context;
    private readonly IValidator<Sexo> _validator;

    public SexosAppServices(AppDbContext context, IValidator<Sexo> validator)
    {
        _context = context;
        _validator = validator;
    }

    public List<Sexo> BuscaSexos()
    {

        var sexos = _context.Sexos.AsNoTrackingWithIdentityResolution().ToList();

        return sexos;
    }

    public Sexo BuscaSexo(int id)
    {
        var sexo = _context.Sexos.AsNoTrackingWithIdentityResolution().FirstOrDefault(s => s.SexoId == id);

        return sexo;
    }

    public IEnumerable<string> SalvaSexo(Sexo sexo)
    {
        ValidationResult result = _validator.Validate(sexo);

        if (!result.IsValid)
        {
            var message = result.Errors.Select(e => e.ErrorMessage);
            return message;
        }

        _context.Sexos.Add(sexo);
        _context.SaveChanges();
        return null;
    }

    public IEnumerable<string> AtualizaSexo(int id, Sexo sexo)
    {
        ValidationResult result = _validator.Validate(sexo);

        if (!result.IsValid)
        {
            var message = result.Errors.Select(e => e.ErrorMessage);
            return message;
        }

        var sexoAntigo = _context.Sexos.Find(id);
        if (sexoAntigo is null)
        {
            List<string> message = new List<string>();
            message.Add("Sexo não encontrado...");
            return message;
        }

        sexoAntigo.Genero = sexo.Genero;

        _context.Update(sexoAntigo);
        _context.SaveChanges();
        return null;
    }

    public Sexo DeletaSexo(int id)
    {
        var sexo = _context.Sexos.FirstOrDefault(s => s.SexoId == id);
        if (sexo is null)
        {
            return sexo;
        }

        _context.Remove(sexo);
        _context.SaveChanges();
        return sexo;
    }
}
