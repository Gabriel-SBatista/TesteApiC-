using APICBDA.Context;
using APICBDA.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICBDA.Controllers;

[ApiController]
[Route("[controller]")]
public class ProvasController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IValidator<Prova> _validator;

    public ProvasController(AppDbContext context, IValidator<Prova> validator)
    {
        _context = context;
        _validator = validator;
    }

    [HttpGet]

    public ActionResult<IEnumerable<Prova>> Get()
    {
        var provas = _context.Provas.AsNoTrackingWithIdentityResolution().ToList();
        if (provas is null)
        {
            return NotFound();
        }

        return provas;

    }

    [HttpGet("{id:int}", Name = "ObterProva")]

    public ActionResult<Prova> Get(int id)
    {

        var prova = _context.Provas.AsNoTrackingWithIdentityResolution().FirstOrDefault(p => p.ProvaId == id);
        if (prova is null)
        {
            return NotFound("Prova não encontrada...");
        }

        return prova;
    }

    [HttpPost]

    public ActionResult Post(Prova prova)
    {
        ValidationResult result = _validator.Validate(prova);

        if(!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        _context.Provas.Add(prova);
        _context.SaveChanges();

        return new CreatedAtRouteResult("ObterProva", new { id = prova.ProvaId }, prova);
    }

    [HttpPut("{id:int}")]

    public ActionResult Put(int id, Prova prova)
    {
        if (id != prova.ProvaId)
        {
            return BadRequest();
        }

        _context.Entry(prova).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(prova);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var prova = _context.Provas.FirstOrDefault(p => p.ProvaId == id);

        if (prova is null)
        {
            return NotFound("Prova não encontrada...");
        }

        _context.Remove(prova);
        _context.SaveChanges();

        return Ok(prova);
    }
}
