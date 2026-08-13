# TenantAPI

API en ASP.NET Core para llevar el control de apartamentos y consumo eléctrico mensual en un edificio.

## Sobre este proyecto

Este trabajo corresponde al **segundo parcial práctico** de la materia INF-422.

- **Estudiante:** Kelvin Pina  
- **Matrícula:** 22-1083  
- **Materia:** S-3-2025-INF-422-02

## Qué resuelve

La API permite:

- Registrar apartamentos y dueños.
- Registrar consumos eléctricos por mes.
- Consultar, editar y eliminar datos (CRUD completo).
- Validar datos antes de guardar.

## Tecnologías

- ASP.NET Core 9
- Entity Framework Core 9
- SQL Server LocalDB
- Swagger / OpenAPI

## Endpoints principales

### Apartamentos

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/apartments` | Lista todos los apartamentos |
| GET | `/api/apartments/{id}` | Obtiene un apartamento por ID |
| POST | `/api/apartments` | Crea un apartamento |
| PUT | `/api/apartments/{id}` | Actualiza un apartamento |
| DELETE | `/api/apartments/{id}` | Elimina un apartamento |

### Consumo eléctrico

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/electricityconsumption` | Lista los consumos |
| GET | `/api/electricityconsumption/{id}` | Obtiene un consumo por ID |
| GET | `/api/electricityconsumption/apartment/{id}` | Lista consumos de un apartamento |
| GET | `/api/electricityconsumption/statistics` | Muestra estadísticas |
| POST | `/api/electricityconsumption` | Registra un consumo |
| PUT | `/api/electricityconsumption/{id}` | Actualiza un consumo |
| DELETE | `/api/electricityconsumption/{id}` | Elimina un consumo |

## Cómo ejecutar

```bash
cd TenantAPI
dotnet restore
dotnet run
```

## Swagger

Al iniciar el proyecto, abre la URL local que muestra la consola para entrar a Swagger UI y probar los endpoints.