using System.ComponentModel.DataAnnotations;

namespace TenantAPI.Models
{
    public class Apartment
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El numero de apartamento es requerido")]
        [StringLength(10, ErrorMessage = "El numero de apartamento no puede exceder 10 caracteres")]
        public required string IdApartament { get; set; }

        [Required(ErrorMessage = "El nombre del dueno es requerido")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "El telefono es requerido")]
        [Phone(ErrorMessage = "Formato de telefono invalido")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "El telefono debe tener entre 10 y 15 digitos")]
        public required string Telefono { get; set; }

        // Relacion con consumos electricos
        public ICollection<ElectricityConsumption> ElectricityConsumptions { get; set; } = new List<ElectricityConsumption>();
    }
}