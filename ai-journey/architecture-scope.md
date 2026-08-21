# Architecture scope

The selected structure uses a production-style modular-monolith layout, but the implementation is intentionally constrained to the needs of this exercise.

## Included patterns

- API as composition root.
- Module-first source layout.
- Domain/Application/Infrastructure/Presentation projects per module.
- CQRS request and handler abstractions built on MediatR.
- Result/Error representation for expected failures.
- Domain entities with controlled mutation.
- Explicit module registration.
- A focused dependency rule between modules.

## Deliberately omitted

- External databases, EF Core, Dapper, migrations, and Unit of Work.
- Event buses and integration-event assemblies.
- Inbox/outbox processing and idempotent decorators.
- Distributed caching.
- Authentication and authorization infrastructure.
- Background processing.
- Production observability infrastructure.
- Domain-event dispatch and MediatR pipeline behaviors.
- A comprehensive architecture and integration test matrix.

## Rationale

The exercise has only four use cases and a short time box. Additional runtime mechanisms would obscure the required behavior. Explicit project boundaries directly demonstrate modularity, DDD, and CQRS, while thread-safe in-memory adapters keep the execution flow small and easy to verify.

