using APICBDA.Context;
using APICBDA.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace APICBDA.Services;

public class EstilosAppServices
{
        private readonly AppDbContext _context;
        private readonly IValidator<Estilo> _validator;

        public EstilosAppServices(AppDbContext context, IValidator<Estilo> validator)
        {
            _context = context;
            _validator = validator;
        }

        public List<Estilo> BuscaProvaEstilos()
        {
            var estilos = _context.Estilos.Include(e => e.Provas).AsNoTrackingWithIdentityResolution().ToList();

            return estilos;
        }

        public List<Estilo> BuscaEstilos()
        {

            var estilos = _context.Estilos.AsNoTrackingWithIdentityResolution().ToList();

            return estilos;
        }

        public Estilo BuscaEstilo(int id)
        {
            var estilo = _context.Estilos.AsNoTrackingWithIdentityResolution().FirstOrDefault(e => e.EstiloId == id);

            return estilo;
        }

        public IEnumerable<string> SalvaEstilo(Estilo estilo)
        {
            ValidationResult result = _validator.Validate(estilo);

            if (!result.IsValid)
            {
                var message = result.Errors.Select(e => e.ErrorMessage);
                return message;
            }

            _context.Estilos.Add(estilo);
            _context.SaveChanges();
            return null;
        }

        public IEnumerable<string> AtualizaEstilo(int id, Estilo estilo)
        {
            ValidationResult result = _validator.Validate(estilo);

            if (!result.IsValid)
            {
                var message = result.Errors.Select(e => e.ErrorMessage);
                return message;
            }

            var estiloAntigo = _context.Estilos.Find(id);
            if (estiloAntigo is null)
            {
                List<string> message = new List<string>();
                message.Add("Estilo não encontrado...");
                return message;
            }

            estiloAntigo.Nome = estilo.Nome;

            _context.Update(estiloAntigo);
            _context.SaveChanges();
            return null;
        }

        public Estilo DeletaEstilo(int id)
        {
            var estilo = _context.Estilos.FirstOrDefault(e => e.EstiloId == id);
            if(estilo is null)
            {
                return estilo;
            }

            _context.Remove(estilo);
            _context.SaveChanges();
            return estilo;
        }
}
