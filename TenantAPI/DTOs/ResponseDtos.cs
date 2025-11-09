namespace TenantAPI.DTOs
{
    public class ApartmentDto
    {
        public int Id { get; set; }
        public required string IdApartament { get; set; }
        public required string Nombre { get; set; }
        public required string Telefono { get; set; }
        public List<ElectricityConsumptionDto> ElectricityConsumptions { get; set; } = new List<ElectricityConsumptionDto>();
    }

    public class ElectricityConsumptionDto
    {
        public int Id { get; set; }
        public int IdApartamento { get; set; }
        public DateTime Fecha { get; set; }
        public decimal CantidadKw { get; set; }
        public ApartmentBasicDto? Apartment { get; set; }
    }

    public class ApartmentBasicDto
    {
        public int Id { get; set; }
        public required string IdApartament { get; set; }
        public required string Nombre { get; set; }
        public required string Telefono { get; set; }
    }
}