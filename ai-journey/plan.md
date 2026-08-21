# Implementation plan

## Goal

Deliver a small .NET 8 modular monolith with FastEndpoints, DDD-oriented domain models, CQRS, in-memory persistence, four required operations, a few meaningful tests, local run instructions, and a transparent AI journey. Use explicit module/layer projects while keeping the runtime flow intentionally simple.

## Work packages

| Chat | Scope | Primary output | Depends on |
| --- | --- | --- | --- |
| 1 | Layered foundation and composition contracts | Buildable solution and module registration seams | None |
| 2 | Users module | Create/get user vertical slices and tests | Chat 1 |
| 3 | WorkItems module | Create/list work item vertical slices and tests | Chat 1 |
| 4 | Integration and API behavior | Cross-module wiring and endpoint-level tests | Chats 2 and 3 |
| 5 | Documentation and final QA | README, HTTP examples, AI record, verification report | Chats 1–4 |

## Definition of done

- `dotnet build` succeeds.
- `dotnet test` succeeds.
- The service starts locally on .NET 8.
- All four operations can be exercised from a checked-in `.http` file.
- Creating a work item rejects an unknown user through a public Users contract.
- Feature modules do not access each other's repositories, entities, or infrastructure.
- Endpoints contain transport concerns only; application handlers implement use cases.
- Domain objects enforce their own local invariants.
- The main README explains boundaries, CQRS flow, trade-offs, and future improvements.
- `ai-journey/` contains the plan, prompts, toolchain, and judgment notes.

## Explicit non-goals

- Authentication or authorization.
- Docker, CI/CD, metrics infrastructure, or production secret management.
- A real database or migrations.
- Exhaustive test coverage.
- Event sourcing, a message broker, distributed services, or generic repository frameworks.
- Domain/integration events, inbox/outbox, Unit of Work, MediatR pipeline behaviors, or unrelated production infrastructure.
