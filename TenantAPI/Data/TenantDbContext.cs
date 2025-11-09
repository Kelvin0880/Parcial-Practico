using Microsoft.EntityFrameworkCore;
using TenantAPI.Models;

namespace TenantAPI.Data
{
    public class TenantDbContext : DbContext
    {
        public TenantDbContext(DbContextOptions<TenantDbContext> options)
            : base(options)
        {
        }

        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<ElectricityConsumption> ElectricityConsumptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar relaciones
            modelBuilder.Entity<ElectricityConsumption>()
                .HasOne(e => e.Apartment)
                .WithMany(a => a.ElectricityConsumptions)
                .HasForeignKey(e => e.IdApartamento)
                .OnDelete(DeleteBehavior.Cascade);

            // Datos iniciales dominicanos autenticos
            modelBuilder.Entity<Apartment>().HasData(
                new Apartment { Id = 1, IdApartament = "101", Nombre = "Rafael Tavares Medina", Telefono = "8092457896" },
                new Apartment { Id = 2, IdApartament = "102", Nombre = "Carmen Peña Rodriguez", Telefono = "8095681234" },
                new Apartment { Id = 3, IdApartament = "201", Nombre = "Miguel Santos Jimenez", Telefono = "8298764523" },
                new Apartment { Id = 4, IdApartament = "202", Nombre = "Yolanda Herrera Castillo", Telefono = "8494512367" },
                new Apartment { Id = 5, IdApartament = "301", Nombre = "Franklin Gutierrez Mora", Telefono = "8097835642" },
                new Apartment { Id = 6, IdApartament = "302", Nombre = "Esperanza Vasquez Luna", Telefono = "8292378459" },
                new Apartment { Id = 7, IdApartament = "401", Nombre = "Domingo Pacheco Vargas", Telefono = "8496547832" },
                new Apartment { Id = 8, IdApartament = "402", Nombre = "Miguelina Rosario Diaz", Telefono = "8095432876" },
                new Apartment { Id = 9, IdApartament = "501", Nombre = "Eugenio Mercado Silva", Telefono = "8297654321" },
                new Apartment { Id = 10, IdApartament = "502", Nombre = "Amparo Contreras Mejia", Telefono = "8493876542" }
            );

            modelBuilder.Entity<ElectricityConsumption>().HasData(
                new ElectricityConsumption { Id = 1, IdApartamento = 1, Fecha = new DateTime(2024, 10, 15), CantidadKw = 287.50m },
                new ElectricityConsumption { Id = 2, IdApartamento = 2, Fecha = new DateTime(2024, 10, 15), CantidadKw = 334.25m },
                new ElectricityConsumption { Id = 3, IdApartamento = 3, Fecha = new DateTime(2024, 10, 15), CantidadKw = 412.80m },
                new ElectricityConsumption { Id = 4, IdApartamento = 4, Fecha = new DateTime(2024, 10, 15), CantidadKw = 298.70m },
                new ElectricityConsumption { Id = 5, IdApartamento = 5, Fecha = new DateTime(2024, 10, 15), CantidadKw = 567.35m },
                new ElectricityConsumption { Id = 6, IdApartamento = 1, Fecha = new DateTime(2024, 9, 15), CantidadKw = 315.45m },
                new ElectricityConsumption { Id = 7, IdApartamento = 2, Fecha = new DateTime(2024, 9, 15), CantidadKw = 289.60m },
                new ElectricityConsumption { Id = 8, IdApartamento = 3, Fecha = new DateTime(2024, 9, 15), CantidadKw = 378.90m },
                new ElectricityConsumption { Id = 9, IdApartamento = 6, Fecha = new DateTime(2024, 10, 15), CantidadKw = 445.20m },
                new ElectricityConsumption { Id = 10, IdApartamento = 7, Fecha = new DateTime(2024, 10, 15), CantidadKw = 523.75m }
            );
        }
    }
}