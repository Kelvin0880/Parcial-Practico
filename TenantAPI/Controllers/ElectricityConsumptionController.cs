using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TenantAPI.Data;
using TenantAPI.Models;
using TenantAPI.DTOs;

namespace TenantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectricityConsumptionController : ControllerBase
    {
        private readonly TenantDbContext _context;

        public ElectricityConsumptionController(TenantDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene la lista de consumos electricos por apartamento y mes
        /// </summary>
        /// <returns>Lista completa de consumos electricos</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ElectricityConsumptionDto>>> GetElectricityConsumptions()
        {
            try
            {
                var consumptions = await _context.ElectricityConsumptions
                    .Include(e => e.Apartment)
                    .OrderByDescending(e => e.Fecha)
                    .ThenBy(e => e.Apartment.IdApartament)
                    .ToListAsync();

                var consumptionDtos = consumptions.Select(e => new ElectricityConsumptionDto
                {
                    Id = e.Id,
                    IdApartamento = e.IdApartamento,
                    Fecha = e.Fecha,
                    CantidadKw = e.CantidadKw,
                    Apartment = new ApartmentBasicDto
                    {
                        Id = e.Apartment.Id,
                        IdApartament = e.Apartment.IdApartament,
                        Nombre = e.Apartment.Nombre,
                        Telefono = e.Apartment.Telefono
                    }
                }).ToList();

                return Ok(consumptionDtos);
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
        /// Obtiene el consumo electrico de un apartamento en particular
        /// </summary>
        /// <param name="id">ID del registro de consumo</param>
        /// <returns>Datos del consumo electrico especifico</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ElectricityConsumptionDto>> GetElectricityConsumption(int id)
        {
            try
            {
                var consumption = await _context.ElectricityConsumptions
                    .Include(e => e.Apartment)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (consumption == null)
                {
                    return NotFound(new { 
                        message = "Registro de consumo electrico no encontrado",
                        consumptionId = id 
                    });
                }

                var consumptionDto = new ElectricityConsumptionDto
                {
                    Id = consumption.Id,
                    IdApartamento = consumption.IdApartamento,
                    Fecha = consumption.Fecha,
                    CantidadKw = consumption.CantidadKw,
                    Apartment = new ApartmentBasicDto
                    {
                        Id = consumption.Apartment.Id,
                        IdApartament = consumption.Apartment.IdApartament,
                        Nombre = consumption.Apartment.Nombre,
                        Telefono = consumption.Apartment.Telefono
                    }
                };

                return Ok(consumptionDto);
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
        /// Obtiene los consumos electricos por apartamento
        /// </summary>
        /// <param name="apartmentId">ID del apartamento</param>
        /// <returns>Lista de consumos del apartamento especifico</returns>
        [HttpGet("apartment/{apartmentId}")]
        public async Task<ActionResult<IEnumerable<ElectricityConsumption>>> GetConsumptionsByApartment(int apartmentId)
        {
            try
            {
                // Verificar que el apartamento existe
                var apartment = await _context.Apartments.FindAsync(apartmentId);
                if (apartment == null)
                {
                    return NotFound(new { 
                        message = "Apartamento no encontrado",
                        apartmentId = apartmentId 
                    });
                }

                var consumptions = await _context.ElectricityConsumptions
                    .Include(e => e.Apartment)
                    .Where(e => e.IdApartamento == apartmentId)
                    .OrderByDescending(e => e.Fecha)
                    .ToListAsync();

                return Ok(consumptions);
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
        /// Registra un nuevo consumo mensual para un apartamento
        /// </summary>
        /// <param name="consumption">Datos del consumo a registrar</param>
        /// <returns>Consumo electrico creado</returns>
        [HttpPost]
        public async Task<ActionResult<ElectricityConsumption>> PostElectricityConsumption(
            ElectricityConsumption consumption)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Verificar que el apartamento existe
                var apartmentExists = await _context.Apartments
                    .AnyAsync(a => a.Id == consumption.IdApartamento);

                if (!apartmentExists)
                {
                    return BadRequest(new { 
                        message = "El apartamento especificado no existe",
                        apartmentId = consumption.IdApartamento 
                    });
                }

                // Verificar que no exista un consumo para el mismo apartamento y mes
                var existingConsumption = await _context.ElectricityConsumptions
                    .FirstOrDefaultAsync(e => e.IdApartamento == consumption.IdApartamento &&
                                           e.Fecha.Month == consumption.Fecha.Month &&
                                           e.Fecha.Year == consumption.Fecha.Year);

                if (existingConsumption != null)
                {
                    return Conflict(new { 
                        message = "Ya existe un registro de consumo para este apartamento en esta fecha",
                        apartmentId = consumption.IdApartamento,
                        month = consumption.Fecha.ToString("MMMM yyyy") 
                    });
                }

                _context.ElectricityConsumptions.Add(consumption);
                await _context.SaveChangesAsync();

                // Cargar el apartamento relacionado para la respuesta
                await _context.Entry(consumption)
                    .Reference(e => e.Apartment)
                    .LoadAsync();

                return CreatedAtAction(nameof(GetElectricityConsumption), 
                    new { id = consumption.Id }, consumption);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    message = "Error al registrar el consumo electrico", 
                    details = ex.Message 
                });
            }
        }

        /// <summary>
        /// Actualiza el consumo registrado de un apartamento
        /// </summary>
        /// <param name="id">ID del registro de consumo</param>
        /// <param name="consumption">Datos actualizados del consumo</param>
        /// <returns>Resultado de la actualizacion</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutElectricityConsumption(
            int id, ElectricityConsumption consumption)
        {
            try
            {
                if (id != consumption.Id)
                {
                    return BadRequest(new { 
                        message = "El ID del consumo no coincide",
                        providedId = id,
                        consumptionId = consumption.Id 
                    });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Verificar que el registro existe
                var existingConsumption = await _context.ElectricityConsumptions.FindAsync(id);
                if (existingConsumption == null)
                {
                    return NotFound(new { 
                        message = "Registro de consumo no encontrado para actualizar",
                        consumptionId = id 
                    });
                }

                // Verificar que el apartamento existe
                var apartmentExists = await _context.Apartments
                    .AnyAsync(a => a.Id == consumption.IdApartamento);

                if (!apartmentExists)
                {
                    return BadRequest(new { 
                        message = "El apartamento especificado no existe",
                        apartmentId = consumption.IdApartamento 
                    });
                }

                // Verificar que no exista otro consumo para el mismo apartamento y mes
                var duplicateConsumption = await _context.ElectricityConsumptions
                    .FirstOrDefaultAsync(e => e.IdApartamento == consumption.IdApartamento &&
                                           e.Fecha.Month == consumption.Fecha.Month &&
                                           e.Fecha.Year == consumption.Fecha.Year &&
                                           e.Id != id);

                if (duplicateConsumption != null)
                {
                    return Conflict(new { 
                        message = "Ya existe otro registro de consumo para este apartamento en esta fecha",
                        apartmentId = consumption.IdApartamento,
                        month = consumption.Fecha.ToString("MMMM yyyy") 
                    });
                }

                _context.Entry(existingConsumption).CurrentValues.SetValues(consumption);

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElectricityConsumptionExists(id))
                {
                    return NotFound(new { 
                        message = "Registro de consumo no encontrado durante la actualizacion",
                        consumptionId = id 
                    });
                }
                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    message = "Error al actualizar el consumo electrico", 
                    details = ex.Message 
                });
            }
        }

        /// <summary>
        /// Elimina un registro de consumo mensual
        /// </summary>
        /// <param name="id">ID del registro de consumo a eliminar</param>
        /// <returns>Resultado de la eliminacion</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElectricityConsumption(int id)
        {
            try
            {
                var consumption = await _context.ElectricityConsumptions
                    .Include(e => e.Apartment)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (consumption == null)
                {
                    return NotFound(new { 
                        message = "Registro de consumo electrico no encontrado para eliminar",
                        consumptionId = id 
                    });
                }

                _context.ElectricityConsumptions.Remove(consumption);
                await _context.SaveChangesAsync();

                return Ok(new { 
                    message = "Registro de consumo eliminado exitosamente",
                    apartmentNumber = consumption.Apartment.IdApartament,
                    ownerName = consumption.Apartment.Nombre,
                    consumptionMonth = consumption.Fecha.ToString("MMMM yyyy"),
                    kwAmount = consumption.CantidadKw 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    message = "Error al eliminar el registro de consumo", 
                    details = ex.Message 
                });
            }
        }

        /// <summary>
        /// Obtiene estadisticas de consumo por apartamento
        /// </summary>
        /// <returns>Estadisticas de consumo electrico</returns>
        [HttpGet("statistics")]
        public async Task<ActionResult> GetConsumptionStatistics()
        {
            try
            {
                var statistics = await _context.ElectricityConsumptions
                    .Include(e => e.Apartment)
                    .GroupBy(e => e.Apartment)
                    .Select(g => new
                    {
                        ApartmentNumber = g.Key.IdApartament,
                        OwnerName = g.Key.Nombre,
                        TotalConsumptions = g.Count(),
                        AverageKw = g.Average(e => e.CantidadKw),
                        MaxKw = g.Max(e => e.CantidadKw),
                        MinKw = g.Min(e => e.CantidadKw),
                        TotalKw = g.Sum(e => e.CantidadKw),
                        LastReading = g.Max(e => e.Fecha)
                    })
                    .OrderByDescending(s => s.TotalKw)
                    .ToListAsync();

                return Ok(statistics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    message = "Error al obtener estadisticas", 
                    details = ex.Message 
                });
            }
        }

        private bool ElectricityConsumptionExists(int id)
        {
            return _context.ElectricityConsumptions.Any(e => e.Id == id);
        }
    }
}