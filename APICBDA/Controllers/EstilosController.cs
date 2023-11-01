using APICBDA.Context;
using APICBDA.Models;
using APICBDA.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICBDA.Controllers;

[ApiController]
[Route("[controller]")]
public class EstilosController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IValidator<Estilo> _validator;

    public EstilosController(AppDbContext context, IValidator<Estilo> validator)
    {
        _context = context;
        _validator = validator;
    }

    [HttpGet("provas")]

    public ActionResult<IEnumerable<Estilo>> GetEstiloProva()
    {
        var application = new EstilosAppServices(_context, _validator);
        var estilos = application.BuscaProvaEstilos();

        if (estilos is null)
        {
            return NotFound();
        }

        return estilos;    
    }

    [HttpGet]
    public ActionResult<IEnumerable<Estilo>> Get()
    {
        var application = new EstilosAppServices(_context, _validator);
        var estilos = application.BuscaEstilos();

            if (estilos is null)
            {
                return NotFound();
            }

            return estilos;       
    }

    [HttpGet("{id:int}", Name="ObterEstilo")]
    public ActionResult<Estilo> Get(int id)
    {
        var application = new EstilosAppServices(_context, _validator);
        var estilo = application.BuscaEstilo(id);
            if (estilo is null)
            {
                return NotFound("Estilo não encontrado...");
            }

            return estilo;
    }

    [HttpPost]
    public ActionResult Post(Estilo estilo)
    {
        var application = new EstilosAppServices(_context, _validator);
        var error = application.SalvaEstilo(estilo);

        if (error is null)
        {
            return Ok(estilo);
        }

        return BadRequest(error);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Estilo estilo)
    {
        var application = new EstilosAppServices(_context, _validator);
        var error = application.AtualizaEstilo(id, estilo);
        
        if (error is null)
        {
            return Ok(estilo);
        }

        return BadRequest(error);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var application = new EstilosAppServices(_context, _validator);
        var estilo = application.DeletaEstilo(id);

        if (estilo is null)
        {
            return NotFound("Estilo não encontrado...");
        }

        return Ok(estilo);
    }
}
