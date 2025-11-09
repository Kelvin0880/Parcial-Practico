using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TenantAPI.Models
{
    public class ElectricityConsumption
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El ID del apartamento es requerido")]
        public int IdApartamento { get; set; }

        [Required(ErrorMessage = "La fecha es requerida")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "La cantidad de kW es requerida")]
        [Range(0.01, double.MaxValue, ErrorMessage = "La cantidad de kW debe ser mayor a 0")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal CantidadKw { get; set; }

        // Relacion con Apartment
        [ForeignKey("IdApartamento")]
        public Apartment? Apartment { get; set; }
    }
}