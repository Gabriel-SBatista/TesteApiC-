using APICBDA.Context;
using APICBDA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICBDA.Controllers;

[ApiController]
[Route("[controller]")]
public class ProvasController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProvasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]

    public ActionResult<IEnumerable<Prova>> Get()
    {
        try
        {
            var provas = _context.Provas.AsNoTracking().ToList();
            if (provas is null)
            {
                return NotFound();
            }

            return provas;
        }
        catch(Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solitação.");
        }   
    }

    [HttpGet("{id:int}", Name="ObterProva")]

    public ActionResult<Prova> Get(int id)
    {
        try
        {
            var prova = _context.Provas.AsNoTracking().FirstOrDefault(p => p.ProvaId == id);
            if (prova is null)
            {
                return NotFound("Prova não encontrada...");
            }

            return prova;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solitação.");
        }       
    }

    [HttpPost]

    public ActionResult Post(Prova prova)
    {
        try
        {
            if (prova is null)
                return BadRequest();

            _context.Provas.Add(prova);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProva", new { id = prova.ProvaId }, prova);
        }
        catch(Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solitação.");
        }      
    }

    [HttpPut("{id:int}")]

    public ActionResult Put(int id, Prova prova)
    {
        try
        {
            if (id != prova.ProvaId)
            {
                return BadRequest();
            }

            _context.Entry(prova).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(prova);
        }
        catch(Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solitação.");
        }     
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        try
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
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solitação.");
        }      
    }
}
