using APICBDA.Context;
using APICBDA.Models;
using APICBDA.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICBDA.Controllers;

[ApiController]
[Route("[controller]")]
public class SexosController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IValidator<Sexo> _validator;

    public SexosController(AppDbContext context, IValidator<Sexo> validator)
    {
        _context = context;
        _validator = validator;
    }

    [HttpGet]

    public ActionResult<IEnumerable<Sexo>> Get()
    {
        var application = new SexosAppServices(_context, _validator);
        var sexos = application.BuscaSexos();

            if (sexos is null)
            {
                return NotFound();
            }

            return sexos;   
    }

    [HttpGet("{id:int}", Name = "ObterSexo")]

    public ActionResult<Sexo> Get(int id)
    {
        var application = new SexosAppServices(_context, _validator);
        var sexo = application.BuscaSexo(id);

            if (sexo is null)
            {
                return NotFound("Sexo não encontrado...");
            }

            return sexo;   
    }

    [HttpPost]

    public ActionResult Post(Sexo sexo)
    {
        var application = new SexosAppServices(_context, _validator);
        var error = application.SalvaSexo(sexo);

        if (error is null)
        {
            return Ok(sexo);
        }

        return BadRequest(error);
    }

    [HttpPut("{id:int}")]

    public ActionResult Put(int id, Sexo sexo)
    {
        var application = new SexosAppServices(_context, _validator);
        var error = application.AtualizaSexo(id, sexo);
        
        if (error is null)
        {
            return Ok(sexo);
        }

        return BadRequest(error);
    }

    [HttpDelete("{id:int}")]

    public ActionResult Delete(int id)
    {
        var application = new SexosAppServices(_context, _validator);
        var sexo = application.DeletaSexo(id);
        
        if (sexo is null)
            return NotFound("Sexo não encontrado...");

        return Ok(sexo);
    }
}
