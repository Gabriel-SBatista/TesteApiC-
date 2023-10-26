using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace APICBDA.Models
{
    public class Estilo
    {
        public Estilo()
        {
            Provas = new Collection<Prova>();
        }
        public int EstiloId { get; set; }
        [Required]
        [MaxLength(20)]
        public string? Nome { get; set; }
        public ICollection<Prova>? Provas { get; set; }
    }
}
