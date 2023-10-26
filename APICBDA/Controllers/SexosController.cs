using APICBDA.Context;
using APICBDA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICBDA.Controllers;

[ApiController]
[Route("[controller]")]
public class SexosController : ControllerBase
{
    private readonly AppDbContext _context;

    public SexosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]

    public ActionResult<IEnumerable<Sexo>> Get()
    {
        try
        {
            var sexos = _context.Sexos.AsNoTracking().ToList();

            if (sexos is null)
            {
                return NotFound();
            }

            return sexos;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solitação.");
        }     
    }

    [HttpGet("{id:int}", Name = "ObterSexo")]

    public ActionResult<Sexo> Get(int id)
    {
        try
        {
            var sexo = _context.Sexos.AsNoTracking().FirstOrDefault(s => s.SexoId == id);

            if (sexo is null)
            {
                return NotFound("Sexo não encontrado...");
            }

            return sexo;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solitação.");
        }      
    }

    [HttpPost]

    public ActionResult Post(Sexo sexo)
    {
        try
        {
            if(sexo is null)
            {
                return BadRequest();
            }
            _context.Sexos.Add(sexo);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterSexo", new { id = sexo.SexoId }, sexo);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solitação.");
        }     
    }

    [HttpPut("{id:int}")]

    public ActionResult Put(int id, Sexo sexo)
    {
        try
        {
            if (id != sexo.SexoId)
            {
                return BadRequest();
            }

            _context.Entry(sexo).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(sexo);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solitação.");
        }      
    }

    [HttpDelete("{id:int}")]

    public ActionResult Delete(int id)
    {
        try
        {
            var sexo = _context.Sexos.FirstOrDefault(s => s.SexoId == id);

            if (sexo is null)
            {
                return NotFound("Sexo não encontrado...");
            }

            _context.Remove(sexo);
            _context.SaveChanges();

            return Ok(sexo);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solitação.");
        }            
    }
}
