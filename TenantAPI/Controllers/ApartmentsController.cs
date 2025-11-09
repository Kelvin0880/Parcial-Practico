using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TenantAPI.Data;
using TenantAPI.Models;
using TenantAPI.DTOs;

namespace TenantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentsController : ControllerBase
    {
        private readonly TenantDbContext _context;

        public ApartmentsController(TenantDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene la lista completa de apartamentos con sus duenos
        /// </summary>
        /// <returns>Lista de apartamentos con consumos electricos</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApartmentDto>>> GetApartments()
        {
            try
            {
                var apartments = await _context.Apartments
                    .Include(a => a.ElectricityConsumptions)
                    .OrderBy(a => a.IdApartament)
                    .ToListAsync();

                var apartmentDtos = apartments.Select(a => new ApartmentDto
                {
                    Id = a.Id,
                    IdApartament = a.IdApartament,
                    Nombre = a.Nombre,
                    Telefono = a.Telefono,
                    ElectricityConsumptions = a.ElectricityConsumptions.Select(e => new ElectricityConsumptionDto
                    {
                        Id = e.Id,
                        IdApartamento = e.IdApartamento,
                        Fecha = e.Fecha,
                        CantidadKw = e.CantidadKw
                    }).ToList()
                }).ToList();

                return Ok(apartmentDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    message = "Error interno del servidor", 
                    details = ex.Message 
                });
            }
        }

        /// <summary>
        /// Obtiene los datos de un apartamento especifico por ID
        /// </summary>
        /// <param name="id">ID del apartamento</param>
        /// <returns>Datos del apartamento con sus consumos</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApartmentDto>> GetApartment(int id)
        {
            try
            {
                var apartment = await _context.Apartments
                    .Include(a => a.ElectricityConsumptions.OrderByDescending(e => e.Fecha))
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (apartment == null)
                {
                    return NotFound(new { 
                        message = "Apartamento no encontrado",
                        apartmentId = id 
                    });
                }

                var apartmentDto = new ApartmentDto
                {
                    Id = apartment.Id,
                    IdApartament = apartment.IdApartament,
                    Nombre = apartment.Nombre,
                    Telefono = apartment.Telefono,
                    ElectricityConsumptions = apartment.ElectricityConsumptions.Select(e => new ElectricityConsumptionDto
                    {
                        Id = e.Id,
                        IdApartamento = e.IdApartamento,
                        Fecha = e.Fecha,
                        CantidadKw = e.CantidadKw
                    }).ToList()
                };

                return Ok(apartmentDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    message = "Error interno del servidor", 
                    details = ex.Message 
                });
            }
        }

        /// <summary>
        /// Crea un nuevo registro de apartamento y dueno
        /// </summary>
        /// <param name="apartment">Datos del apartamento a crear</param>
        /// <returns>Apartamento creado</returns>
        [HttpPost]
        public async Task<ActionResult<Apartment>> PostApartment(Apartment apartment)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Verificar que no exista otro apartamento con el mismo numero
                var existingApartment = await _context.Apartments
                    .FirstOrDefaultAsync(a => a.IdApartament == apartment.IdApartament);

                if (existingApartment != null)
                {
                    return Conflict(new { 
                        message = "Ya existe un apartamento con este numero",
                        apartmentNumber = apartment.IdApartament 
                    });
                }

                _context.Apartments.Add(apartment);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetApartment), 
                    new { id = apartment.Id }, apartment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    message = "Error al crear el apartamento", 
                    details = ex.Message 
                });
            }
        }

        /// <summary>
        /// Actualiza los datos de un apartamento especifico
        /// </summary>
        /// <param name="id">ID del apartamento</param>
        /// <param name="apartment">Datos actualizados del apartamento</param>
        /// <returns>Resultado de la actualizacion</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApartment(int id, Apartment apartment)
        {
            try
            {
                if (id != apartment.Id)
                {
                    return BadRequest(new { 
                        message = "El ID del apartamento no coincide",
                        providedId = id,
                        apartmentId = apartment.Id 
                    });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Verificar que el apartamento existe
                var existingApartment = await _context.Apartments.FindAsync(id);
                if (existingApartment == null)
                {
                    return NotFound(new { 
                        message = "Apartamento no encontrado para actualizar",
                        apartmentId = id 
                    });
                }

                // Verificar que no exista otro apartamento con el mismo numero
                var duplicateApartment = await _context.Apartments
                    .FirstOrDefaultAsync(a => a.IdApartament == apartment.IdApartament && a.Id != id);

                if (duplicateApartment != null)
                {
                    return Conflict(new { 
                        message = "Ya existe otro apartamento con este numero",
                        apartmentNumber = apartment.IdApartament 
                    });
                }

                _context.Entry(existingApartment).CurrentValues.SetValues(apartment);

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApartmentExists(id))
                {
                    return NotFound(new { 
                        message = "Apartamento no encontrado durante la actualizacion",
                        apartmentId = id 
                    });
                }
                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    message = "Error al actualizar el apartamento", 
                    details = ex.Message 
                });
            }
        }

        /// <summary>
        /// Elimina un registro de apartamento
        /// </summary>
        /// <param name="id">ID del apartamento a eliminar</param>
        /// <returns>Resultado de la eliminacion</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApartment(int id)
        {
            try
            {
                var apartment = await _context.Apartments
                    .Include(a => a.ElectricityConsumptions)
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (apartment == null)
                {
                    return NotFound(new { 
                        message = "Apartamento no encontrado para eliminar",
                        apartmentId = id 
                    });
                }

                // Informar sobre registros relacionados que seran eliminados
                var consumptionCount = apartment.ElectricityConsumptions.Count;

                _context.Apartments.Remove(apartment);
                await _context.SaveChangesAsync();

                return Ok(new { 
                    message = "Apartamento eliminado exitosamente",
                    apartmentNumber = apartment.IdApartament,
                    ownerName = apartment.Nombre,
                    deletedConsumptions = consumptionCount 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    message = "Error al eliminar el apartamento", 
                    details = ex.Message 
                });
            }
        }

        private bool ApartmentExists(int id)
        {
            return _context.Apartments.Any(e => e.Id == id);
        }
    }
}