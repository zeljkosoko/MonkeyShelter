      ┌────────────────────┐
      │  MonkeyShelter.API │ ◄────────── HTTP requests (Swagger, UI)
      └────────────────────┘
                │
                ▼
      ┌────────────────────┐
      │ MonkeyShelter.Services │ ◄──────── Business logic
      └────────────────────┘
                │
                ▼
      ┌────────────────────┐
      │ MonkeyShelter.Infrastructure │ ◄── EF Core, Redis, SQL
      └────────────────────┘
                │
                ▼
      ┌────────────────────┐
      │    MonkeyShelter.Core    │ ◄── Entities, interfaces, DTO-s
      └────────────────────┘

🧱 Architecture Layers

1. MonkeyShelter.Core (Domain)
Contains entities such as Monkey, Species, Shelter, VeterinaryCheck.

Interfaces such as IMonkeyRepository, IMonkeyService.

DTO models for communication between layers.

Does not contain any implementation of EF Core.

➡️ Completely isolated from all technologies.

2. MonkeyShelter.Infrastructure (Data Access)
Repository implementation.

EF Core DbContext (MonkeyShelterDbContext).

Entity configurations (flunt mapping).

Caching (e.g. Redis), logic for sending metrics.

➡️ Communicates only with the database and external storage (such as Redis).

3. MonkeyShelter.Services (Business Logic)
Contains services such as MonkeyService, VeterinaryService.

Implements interfaces from the Core layer.

Logic for adding/removing monkeys, scheduling reviews, constraints.

➡️ Knows nothing about controllers or HTTP. Only business logic.

4. MonkeyShelter.API (Presentation Layer / API)
ASP.NET Core Web API controllers.

Swagger documentation.

Middleware for logging, errors.

Dependency Injection configuration.

Automapper configuration.

➡️ Combines all other layers and exposes an HTTP interface.
---------------
Design Patterns Used

Pattern and Explanation:
Repository Pattern- Abstracts data access logic. Allows for easier testing and separation of logic.
Unit of Work (optional)- Can be added if multiple operations are performed in a single SaveChanges() call.
Dependency Injection - All dependencies are injected via the constructor, consistent with the ASP.NET Core DI system.
DTO (Data Transfer Object)- Separates internal logic and data returned to the client (ViewModel).
AutoMapper -For easy mapping between entities and DTOs.
Caching (Redis) -For caching data such as the number of monkeys per species.
