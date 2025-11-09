# TenantAPI - Sistema de Gestion Electrica

**Estudiante:** Kelvin Pina  
**Matricula:** 22-1083  
**Materia:** S-3-2025-INF-422-02  
**Parcial:** Segundo Parcial Practico  

## Descripcion del Proyecto

TenantAPI es una Web API desarrollada en ASP.NET Core para gestionar apartamentos, propietarios y el consumo electrico mensual en el edificio Tenant. El sistema implementa operaciones CRUD completas con validacion de datos y manejo de errores robusto.

## Caracteristicas Principales

- **Gestion de Apartamentos y Propietarios**: CRUD completo para registro de apartamentos
- **Control de Consumo Electrico**: Registro y seguimiento del consumo mensual por apartamento
- **Validacion Avanzada**: Validacion de datos con mensajes de error personalizados
- **Documentacion Swagger**: Interfaz interactiva para pruebas de API
- **Datos Iniciales**: Base de datos pre-poblada con ejemplos dominicanos autenticos
- **Manejo de Errores**: Respuestas HTTP apropiadas con mensajes descriptivos


## Tecnologias Utilizadas

- **ASP.NET Core 9.0** - Framework principal
- **Entity Framework Core 9.0** - ORM para base de datos
- **SQL Server LocalDB** - Base de datos
- **Swagger/OpenAPI** - Documentacion de API
- **AutoMapper** - Mapeo de objetos (configurado para uso futuro)

## Endpoints Disponibles

### Apartamentos (ApartmentsController)

| Metodo | Endpoint | Descripcion |
|--------|----------|-------------|
| GET | `/api/apartments` | Obtiene todos los apartamentos |
| GET | `/api/apartments/{id}` | Obtiene un apartamento especifico |
| POST | `/api/apartments` | Crea un nuevo apartamento |
| PUT | `/api/apartments/{id}` | Actualiza un apartamento |
| DELETE | `/api/apartments/{id}` | Elimina un apartamento |

### Consumo Electrico (ElectricityConsumptionController)

| Metodo | Endpoint | Descripcion |
|--------|----------|-------------|
| GET | `/api/electricityconsumption` | Obtiene todos los consumos |
| GET | `/api/electricityconsumption/{id}` | Obtiene un consumo especifico |
| GET | `/api/electricityconsumption/apartment/{id}` | Obtiene consumos por apartamento |
| GET | `/api/electricityconsumption/statistics` | Obtiene estadisticas de consumo |
| POST | `/api/electricityconsumption` | Registra un nuevo consumo |
| PUT | `/api/electricityconsumption/{id}` | Actualiza un consumo |
| DELETE | `/api/electricityconsumption/{id}` | Elimina un consumo |

## Ejemplos de Uso

### Crear Apartamento

```json
POST /api/apartments
{
  "idApartament": "601",
  "nombre": "Jose Luis Martinez Fernandez",
  "telefono": "8095678901"
}
```

### Registrar Consumo Electrico

```json
POST /api/electricityconsumption
{
  "idApartamento": 1,
  "fecha": "2024-11-15",
  "cantidadKw": 425.75
}
```

## Datos de Prueba Pre-cargados

El sistema incluye 10 apartamentos con propietarios dominicanos autenticos:

1. **Apt 101** - Rafael Tavares Medina (809-245-7896)
2. **Apt 102** - Carmen Peña Rodriguez (809-568-1234)
3. **Apt 201** - Miguel Santos Jimenez (829-876-4523)
4. **Apt 202** - Yolanda Herrera Castillo (849-451-2367)
5. **Apt 301** - Franklin Gutierrez Mora (809-783-5642)
6. **Apt 302** - Esperanza Vasquez Luna (829-237-8459)
7. **Apt 401** - Domingo Pacheco Vargas (849-654-7832)
8. **Apt 402** - Miguelina Rosario Diaz (809-543-2876)
9. **Apt 501** - Eugenio Mercado Silva (829-765-4321)
10. **Apt 502** - Amparo Contreras Mejia (849-387-6542)

## Configuracion de Base de Datos

La aplicacion utiliza SQL Server LocalDB con la siguiente cadena de conexion:
```
Server=(localdb)\\MSSQLLocalDB;Database=TenantDB_KelvinPina_22_1083;Trusted_Connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true
```

## Comandos de Instalacion y Ejecucion

```bash
# Clonar y navegar al proyecto
cd TenantAPI

# Restaurar paquetes
dotnet restore

# Ejecutar migraciones (si es necesario)
dotnet ef database update

# Ejecutar la aplicacion
dotnet run
```

## Validaciones Implementadas

### Modelo Apartment
- **IdApartament**: Requerido, maximo 10 caracteres
- **Nombre**: Requerido, maximo 100 caracteres
- **Telefono**: Requerido, formato telefono valido, 10-15 digitos

### Modelo ElectricityConsumption
- **IdApartamento**: Requerido, debe existir en la tabla Apartments
- **Fecha**: Requerida, formato de fecha valido
- **CantidadKw**: Requerida, debe ser mayor a 0, formato decimal(18,2)

## Manejo de Errores

La API maneja varios tipos de errores:

- **400 Bad Request**: Datos de entrada invalidos
- **404 Not Found**: Recurso no encontrado
- **409 Conflict**: Conflicto de datos (duplicados)
- **500 Internal Server Error**: Errores del servidor



## Caracteristicas Avanzadas

- **Relaciones de Datos**: Foreign Keys con cascada
- **Consultas Optimizadas**: Include para cargar datos relacionados
- **Paginacion**: Preparado para implementar paginacion
- **Estadisticas**: Endpoint de estadisticas de consumo
- **Logging**: Configurado para desarrollo y produccion
- **CORS**: Habilitado para desarrollo

## Acceso a Swagger

Una vez ejecutada la aplicacion, puedes acceder a la documentacion interactiva en:
- **URL Local**: `https://localhost:xxxx/`
- **Swagger UI**: Interfaz completa para probar todos los endpoints

## Mejores Practicas Implementadas

1. **Separacion de Responsabilidades**: Controladores, modelos y contexto separados
2. **Validacion Robusta**: Validaciones tanto en modelos como en controladores
3. **Manejo de Excepciones**: Try-catch en todos los endpoints
4. **Nombres Descriptivos**: Variables y metodos con nombres claros
5. **Documentacion XML**: Comentarios para Swagger
6. **Convencion de Nombres**: Seguimiento de convenciones de C#
7. **Inyeccion de Dependencias**: DbContext inyectado correctamente
8. **Configuracion Centralizada**: appsettings.json para configuraciones

## Notas del Desarrollador


**Desarrollado por Kelvin Pina - Matricula 22-1083**  
**Segundo Parcial Practico - INF-422**  
**Noviembre 2025**