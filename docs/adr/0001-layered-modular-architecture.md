# ADR-0001: Use layered projects within each module

- Status: Accepted
- Date: 2026-08-21

## Context

Using one assembly per feature module would minimize ceremony, but most module and layer boundaries would then be conventions rather than compile-time constraints.

The exercise explicitly evaluates modular-monolith boundaries, DDD, CQRS, maintainability, and clarity. We therefore want those boundaries to be visible in the solution structure. At the same time, the service has only four use cases and a two-to-four-hour intended scope, so the runtime flow and infrastructure must remain deliberately small.

## Decision

### Architectural style

Use one executable API as the composition root. Organize production code by business module, then divide each module into `Domain`, `Application`, `Infrastructure`, and `Presentation` projects.

Use the following supporting patterns:

- use-case folders containing a command/query, handler, validator where needed, and response model;
- MediatR-backed `ICommand`, `IQuery`, and handler abstractions;
- explicit `Result` and `Error` objects for expected business failures;
- one registration entry point per module;
- domain entities with private state mutation and deliberate factory methods.

### Scope control

Do not introduce mechanisms that the four required operations do not need:

- domain events or integration events;
- inbox/outbox processing, event buses, or idempotent event decorators;
- external databases, caches, identity providers, or message brokers;
- EF Core, Dapper, schemas, migrations, or a Unit of Work;
- background jobs or production observability infrastructure;
- MediatR pipeline behaviors;
- generic repositories or base aggregate hierarchies.

These omissions are deliberate trade-offs rather than incomplete production features.

### Project structure

```text
src/
  API/
    BackendTakeHome.Api/
  Common/
    BackendTakeHome.Common.Domain/
    BackendTakeHome.Common.Application/
  Modules/
    Users/
      BackendTakeHome.Modules.Users.Contracts/
      BackendTakeHome.Modules.Users.Domain/
      BackendTakeHome.Modules.Users.Application/
      BackendTakeHome.Modules.Users.Infrastructure/
      BackendTakeHome.Modules.Users.Presentation/
    WorkItems/
      BackendTakeHome.Modules.WorkItems.Domain/
      BackendTakeHome.Modules.WorkItems.Application/
      BackendTakeHome.Modules.WorkItems.Infrastructure/
      BackendTakeHome.Modules.WorkItems.Presentation/
tests/
  BackendTakeHome.Modules.Users.UnitTests/
  BackendTakeHome.Modules.WorkItems.UnitTests/
  BackendTakeHome.Api.IntegrationTests/
```

The project count is justified by visible compile-time boundaries. The implementation inside each project remains intentionally small.

### Dependency rules

- `Common.Domain` has no project dependency.
- `Common.Application` depends only on `Common.Domain` and MediatR.
- A module's Domain depends only on `Common.Domain`.
- A module's Application depends on its Domain and `Common.Application`.
- A module's Presentation depends on its Application and FastEndpoints-related abstractions.
- A module's Infrastructure depends on its Domain and Application and owns module registration.
- The API references module Infrastructure and Presentation projects only for composition and endpoint discovery.
- WorkItems Application may reference `Users.Contracts`; no other WorkItems project references Users internals.

Add one small automated architecture test only if it can reliably enforce the most important rule: WorkItems must not depend on Users Domain, Application, Infrastructure, or Presentation. A comprehensive architecture-test suite is outside the exercise scope.

### Request flow

Keep the execution path short:

```text
HTTP request
  -> FastEndpoints endpoint and request validation
  -> MediatR command or query
  -> one application handler
  -> module repository abstraction
  -> thread-safe in-memory repository
  -> Result/Error mapped to HTTP response
```

There are no domain-event handlers, integration-event consumers, Unit of Work, pipeline behaviors, or background jobs.

### Domain and application rules

- `User` is an entity with a generated `Guid` and a non-empty normalized username.
- `WorkItem` is an entity with a generated `Guid`, a non-empty name, and an assignee user ID.
- Entity factories enforce local invariants.
- Expected failures are represented with `Result`/`Error`.
- The CreateWorkItem handler checks assignee existence using a small interface from `Users.Contracts`.
- Listing by assignee returns an empty list for a valid ID with no matches. Creation returns not-found when the assignee does not exist.
- Repository interfaces belong to the module Domain; their in-memory implementations belong to Infrastructure.
- In-memory repositories are singleton and thread-safe. Data loss on restart is an accepted trade-off.

### CQRS and endpoints

Use MediatR behind small common abstractions:

- `CreateUserCommand` -> `Result<Guid>`.
- `GetUserByIdQuery` -> `Result<UserResponse>`.
- `CreateWorkItemCommand` -> `Result<Guid>`.
- `GetWorkItemsByAssigneeQuery` -> `Result<IReadOnlyCollection<WorkItemResponse>>`.

Expose the required API through FastEndpoints:

- `POST /api/users` -> `201 Created`.
- `GET /api/users/{id}` -> `200 OK` or `404 Not Found`.
- `POST /api/work-items` -> `201 Created`, validation failure, or missing-assignee `404 Not Found`.
- `GET /api/work-items?assigneeId={userId}` -> `200 OK` with a collection.

Endpoints contain transport mapping only and do not expose domain entities.

### Validation and testing

Use FastEndpoints/FluentValidation at the transport boundary for malformed requests, while domain factories remain the final guard for invariants. Avoid a MediatR validation behavior because it lengthens the flow without adding useful signal for four operations.

Tests focus on:

- domain invariants;
- command/query handler behavior;
- missing-assignee behavior through the Users contract;
- one HTTP integration flow covering create user, retrieve user, create work item, and list work items.

## Consequences

### Positive

- Module and layer boundaries are visible and partly enforced by project references.
- The only cross-module dependency is an explicit public contract.
- The runtime flow remains easy to explain and test.
- Infrastructure can later be replaced module by module.
- Explicit omissions demonstrate scope judgment.

### Trade-offs

- The project count is high relative to four endpoints.
- Some projects will contain only a handful of files.
- MediatR adds a dependency for a flow that could technically use direct handler calls.
- In-memory stores cannot demonstrate transactions or durable uniqueness guarantees.

We accept these costs because the assignment explicitly evaluates module boundaries, DDD, and CQRS, while additional infrastructure would distract from those signals.
