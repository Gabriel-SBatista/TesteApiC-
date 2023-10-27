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
            var sexos = _context.Sexos.AsNoTracking().ToList();

            if (sexos is null)
            {
                return NotFound();
            }

            return sexos;   
    }

    [HttpGet("{id:int}", Name = "ObterSexo")]

    public ActionResult<Sexo> Get(int id)
    {
            var sexo = _context.Sexos.AsNoTracking().FirstOrDefault(s => s.SexoId == id);

            if (sexo is null)
            {
                return NotFound("Sexo não encontrado...");
            }

            return sexo;   
    }

    [HttpPost]

    public ActionResult Post(Sexo sexo)
    {
            if(sexo is null)
            {
                return BadRequest();
            }
            _context.Sexos.Add(sexo);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterSexo", new { id = sexo.SexoId }, sexo);   
    }

    [HttpPut("{id:int}")]

    public ActionResult Put(int id, Sexo sexo)
    {
            if (id != sexo.SexoId)
            {
                return BadRequest();
            }

            _context.Entry(sexo).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(sexo);    
    }

    [HttpDelete("{id:int}")]

    public ActionResult Delete(int id)
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
}
