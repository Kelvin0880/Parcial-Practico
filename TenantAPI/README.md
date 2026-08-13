# TenantAPI

API desarrollada en ASP.NET Core para gestionar apartamentos, propietarios y consumo eléctrico mensual.

## Perfil del proyecto

- **Estudiante:** Kelvin Pina  
- **Matrícula:** 22-1083  
- **Materia:** S-3-2025-INF-422-02  
- **Parcial:** Segundo Parcial Práctico  

## Qué incluye

- CRUD de apartamentos y dueños.
- CRUD de consumo eléctrico por mes.
- Validaciones de datos y manejo de errores HTTP.
- Swagger para pruebas rápidas de endpoints.

## Tecnologías

- ASP.NET Core 9.0
- Entity Framework Core 9.0
- SQL Server LocalDB
- Swagger / OpenAPI

## Endpoints principales

### Apartamentos

| Método | Endpoint |
|---|---|
| GET | `/api/apartments` |
| GET | `/api/apartments/{id}` |
| POST | `/api/apartments` |
| PUT | `/api/apartments/{id}` |
| DELETE | `/api/apartments/{id}` |

### Consumo eléctrico

| Método | Endpoint |
|---|---|
| GET | `/api/electricityconsumption` |
| GET | `/api/electricityconsumption/{id}` |
| GET | `/api/electricityconsumption/apartment/{id}` |
| GET | `/api/electricityconsumption/statistics` |
| POST | `/api/electricityconsumption` |
| PUT | `/api/electricityconsumption/{id}` |
| DELETE | `/api/electricityconsumption/{id}` |

## Cómo ejecutarlo

```bash
cd TenantAPI
dotnet restore
dotnet ef database update
dotnet run
```

## Ejemplo rápido

```json
POST /api/apartments
{
  "idApartament": "601",
  "nombre": "Jose Luis Martinez Fernandez",
  "telefono": "8095678901"
}
```

## Acceso a Swagger

Con la API en ejecución, abre la ruta raíz del proyecto para usar la interfaz Swagger.