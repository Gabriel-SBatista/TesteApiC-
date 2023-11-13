using APICBDA.Context;
using APICBDA.Models;
using APICBDA.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

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
        var application = new ProvasAppServices(_context, _validator);
        var provas = application.BuscaProvas();

        if (provas is null)
        {
            return NotFound();
        }

        return provas;
    }

    [HttpGet("{id:int}", Name = "ObterProva")]

    public ActionResult<Prova> Get(int id)
    {
        var application = new ProvasAppServices(_context, _validator);
        var prova = application.BuscaProva(id);

        if (prova is null)
        {
            return NotFound("Prova não encontrada...");
        }

        return prova;
    }

    [HttpPost]

    public ActionResult Post(Prova prova)
    {
        var application = new ProvasAppServices(_context, _validator);
        var error = application.SalvaProva(prova);

        if (error is null)
        {
            return Ok(prova);
        }

        return BadRequest(error);
    }

    [HttpPut("{id:int}")]

    public ActionResult Put(int id, Prova prova)
    {
        var application = new ProvasAppServices(_context, _validator);
        var error = application.AtualizaProva(id, prova);

        if (error is null)
        {
            return Ok(prova);
        }

        return BadRequest(error);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var application = new ProvasAppServices(_context, _validator);
        var prova = application.DeletaProva(id);

        if (prova is null)
        {
            return NotFound("Prova não encontrada...");
        }

        return Ok(prova);
    }
}
