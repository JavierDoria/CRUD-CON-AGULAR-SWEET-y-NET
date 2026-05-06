using System.ComponentModel.DataAnnotations;

namespace MiWebAPI.Entidades
{
    public class Zapatillas
    {
        [Key]
        public int Id {  get; set; }
        public required string Nombre { get; set; }
    }
}
