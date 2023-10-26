﻿using APICBDA.Context;
using APICBDA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICBDA.Controllers;

[ApiController]
[Route("[controller]")]
public class EstilosController : ControllerBase
{
    private readonly AppDbContext _context;

    public EstilosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("provas")]

    public ActionResult<IEnumerable<Estilo>> GetEstiloProva()
    {
        try
        {
            var estilos = _context.Estilos.Include(p => p.Provas).AsNoTracking().ToList();
            if (estilos is null)
            {
                return NotFound();
            }

            return estilos;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solitação.");
        }      
    }

    [HttpGet]
    public ActionResult<IEnumerable<Estilo>> Get()
    {
        try
        {
            var estilos = _context.Estilos.AsNoTracking().ToList();
            if (estilos is null)
            {
                return NotFound();
            }

            return estilos;
        }
        catch(Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solitação.");
        }
        
    }

    [HttpGet("{id:int}", Name="ObterEstilo")]
    public ActionResult<Estilo> Get(int id)
    {
        try
        {
            var estilo = _context.Estilos.AsNoTracking().FirstOrDefault(e => e.EstiloId == id);
            if (estilo is null)
            {
                return NotFound("Estilo não encontrado...");
            }

            return estilo;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solitação.");
        }      
    }

    [HttpPost]
    public ActionResult Post(Estilo estilo)
    {
        try
        {
            if (estilo is null)
                return BadRequest();


            _context.Estilos.Add(estilo);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterEstilo", new { id = estilo.EstiloId }, estilo);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solitação.");
        }      
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Estilo estilo)
    {
        try
        {
            var estiloAntigo = _context.Estilos.Find(id);
            if (estiloAntigo is null)
            {
                return NotFound("Estilo não encontrado...");
            }

            estiloAntigo.Nome = estilo.Nome;

            _context.Update(estiloAntigo);
            _context.SaveChanges();
            return Ok(estiloAntigo);
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
            var estilo = _context.Estilos.FirstOrDefault(e => e.EstiloId == id);

            if (estilo is null)
            {
                return NotFound("Estilo não encontrado...");
            }

            _context.Remove(estilo);
            _context.SaveChanges();

            return Ok(estilo);
        }
        catch(Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solitação.");
        }
        
    }
}