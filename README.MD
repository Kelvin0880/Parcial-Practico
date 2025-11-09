**Rublica**


Segundo Parcial Practico - TenantAPI
Requisitos de finalización
Apertura: sábado, 8 de noviembre de 2025, 08:00
Cierre: viernes, 14 de noviembre de 2025, 23:59
Segundo Parcial - TenantMVC

Mandato

En el edificio Tenant, tienen la necesidad de crear un sistema para registrar los consumos de electricidad cuando la planta entra. Cada apartamento posee su contador de electricidad. Por tanto crear un API para la siguiente funcionalidades:

1. Mantenimiento de Apartamento y Dueño (CRUD)
Crea un controlador ApartmentController con los siguientes endpoints para gestionar los datos de los apartamentos y sus dueños:

GET /api/apartments: Obtiene la lista de apartamentos y dueños.
GET /api/apartments/{id}: Obtiene los datos de un apartamento específico.
POST /api/apartments: Crea un nuevo registro de apartamento y dueño.
PUT /api/apartments/{id}: Actualiza los datos de un apartamento específico.
DELETE /api/apartments/{id}: Elimina un registro de apartamento.
Valida los campos para asegurar que contengan datos válidos (ej. IdApartament, Nombre, Telefono).

Ejemplo: 1. 101, Santiago Ramirez, 8097654321

2. Mantenimiento de Consumo de Electricidad (CRUD)
Crea un controlador ElectricityConsumptionController para registrar el consumo eléctrico por mes:

GET /api/electricityconsumption: Obtiene la lista de consumos eléctricos por apartamento y mes.
GET /api/electricityconsumption/{id}: Obtiene el consumo eléctrico de un apartamento en particular para una fecha específica.
POST /api/electricityconsumption: Registra un nuevo consumo mensual para un apartamento.
PUT /api/electricityconsumption/{id}: Actualiza el consumo registrado de un apartamento.
DELETE /api/electricityconsumption/{id}: Elimina un registro de consumo mensual.
Valida que los campos IdApartamento, Fecha y CantidadKw sean correctos y contengan valores adecuados.

Ejemplo: 1, 2024-07-30, 500.