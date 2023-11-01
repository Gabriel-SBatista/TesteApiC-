using APICBDA.Context;
using APICBDA.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace APICBDA.Services;

public class ProvasAppServices
{
    private readonly AppDbContext _context;
    private readonly IValidator<Prova> _validator;

    public ProvasAppServices(AppDbContext context, IValidator<Prova> validator)
    {
        _context = context;
        _validator = validator;
    }

    public List<Prova> BuscaProvas()
    {

        var provas = _context.Provas.AsNoTrackingWithIdentityResolution().ToList();

        return provas;
    }

    public Prova BuscaProva(int id)
    {
        var prova = _context.Provas.AsNoTrackingWithIdentityResolution().FirstOrDefault(p => p.ProvaId == id);

        return prova;
    }

    public IEnumerable<string> SalvaProva(Prova prova)
    {
        ValidationResult result = _validator.Validate(prova);

        if(!result.IsValid)
        {
            var message = result.Errors.Select(e => e.ErrorMessage);
            return message;
        }

        _context.Provas.Add(prova);
        _context.SaveChanges();
        return null;
    }

    public IEnumerable<string> AtualizaProva(int id, Prova prova)
    {
        ValidationResult result = _validator.Validate(prova);

        if(!result.IsValid)
        {
            var message = result.Errors.Select(e => e.ErrorMessage);
            return message;
        }

        var provaAntiga = _context.Provas.Find(id);
        if (provaAntiga is null)
        {
            List<string> message = new List<string>();
            message.Add("Prova não encontrada...");
            return message;
        }

        provaAntiga.Distancia = prova.Distancia;
        provaAntiga.EstiloId = prova.EstiloId;
        provaAntiga.SexoId = prova.SexoId;

        _context.Update(provaAntiga);
        _context.SaveChanges();
        return null;
    }

    public Prova DeletaProva(int id)
    {
        var prova = _context.Provas.FirstOrDefault(p => p.ProvaId == id);
        if (prova is null)
        {
            return prova;
        }

        _context.Remove(prova);
        _context.SaveChanges();
        return prova;
    }
}
