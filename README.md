# Backend take-home

A small .NET 8 modular monolith implementing four operations with FastEndpoints, MediatR, CQRS, and DDD-oriented domain models. The accepted architecture decision is recorded in [ADR-0001](docs/adr/0001-layered-modular-architecture.md).

## Run and verify

The repository pins the .NET SDK to `8.0.302` (with latest-patch roll-forward).

```powershell
dotnet restore BackendTakeHome.sln
dotnet build BackendTakeHome.sln --no-restore
dotnet test BackendTakeHome.sln --no-build
dotnet run --project src/API/BackendTakeHome.Api/BackendTakeHome.Api.csproj --no-build
```

The API listens on `http://localhost:5080` when launched with the checked-in development profile. With it running, execute [BackendTakeHome.http](BackendTakeHome.http) from top to bottom to exercise all four operations.

## Architecture

`BackendTakeHome.Api` is the composition root. Each business module (`Users` and `WorkItems`) is split into Domain, Application, Infrastructure, and Presentation projects, making the main dependency boundaries compile-time visible. Common Domain and Application projects contain only shared result/error and CQRS abstractions.

The request path is deliberately short:

```text
FastEndpoint -> MediatR command/query -> handler -> repository abstraction
             -> thread-safe in-memory implementation -> Result -> HTTP response
```

Endpoints own validation and HTTP mapping; handlers own use-case orchestration; entities enforce local invariants; Infrastructure owns dependency registration and storage. WorkItems checks an assignee through `Users.Contracts.IUserExistenceChecker`. It does not reference Users domain, application, infrastructure, or presentation internals. This synchronous contract is the only cross-module communication.

## Persistence and trade-offs

Singleton, thread-safe in-memory repositories keep the exercise focused on boundaries and behavior. They are fast and dependency-free, but data is lost at process restart and they do not demonstrate transactions, migrations, or durable uniqueness. The layered project count and MediatR add ceremony for four endpoints; that cost is accepted because the exercise explicitly emphasizes modularity and CQRS.

The service intentionally omits authentication, external infrastructure, events, inbox/outbox, Unit of Work, pipeline behaviors, background jobs, and production observability. These are scope decisions, not implied production readiness.

## With more time

For production needs, replace each in-memory adapter independently with durable persistence and define transaction/concurrency semantics. Then add architecture dependency tests, broader failure-path integration tests, authentication/authorization, observability, and deployment automation based on actual operating requirements. Cross-module integration events would be considered only if synchronous coupling became a measured constraint.
