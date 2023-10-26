using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICBDA.Models;

public class Prova
{
    public int ProvaId { get; set; }
    [Required]
    [Column(TypeName = "smallint")]
    public int Distancia { get; set; }
    public int EstiloId { get; set; }
    [JsonIgnore]
    public Estilo? Estilo { get; set; }
    public int SexoId { get; set; }
    [JsonIgnore]
    public Sexo? Sexo { get; set; }
}
