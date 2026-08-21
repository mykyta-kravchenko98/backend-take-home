# Shared context for every implementation chat

You are contributing to a .NET 8 backend take-home exercise. Read the repository and `docs/adr/0001-layered-modular-architecture.md` before making changes. ADR-0001 is the architectural source of truth.

The required service is a modular monolith using FastEndpoints, CQRS, and DDD-oriented design. It contains:

- Users: create a user by username; get a user by ID.
- WorkItems: create a work item with a name and assignee user ID; list work items assigned to a user ID.

The database may be in memory. The exercise values module boundaries, deliberate domain modeling, clean command/query flow, thin endpoints, maintainability, tests, and clear documentation. It explicitly does not require authentication, UI, Docker, CI/CD, production observability, or exhaustive coverage.

The architecture uses an API composition root, Common Domain/Application building blocks, and Domain/Application/Infrastructure/Presentation projects per module. The complete flow is FastEndpoint -> MediatR command/query -> handler -> in-memory repository -> Result -> HTTP response. There are no domain events, integration events, inbox/outbox, event bus, Unit of Work, database, cache, identity provider, background jobs, or MediatR pipeline behaviors.

General constraints:

1. Stay within the ownership area in your task prompt.
2. Preserve existing user and other-chat changes.
3. Do not introduce project references that violate the ADR.
4. Do not add speculative frameworks or features.
5. Use async APIs and propagate cancellation tokens.
6. Run the most relevant build/tests before finishing.
7. Finish with a concise handoff containing changed files, commands run, results, assumptions, and remaining risks.
8. If the ADR cannot support a necessary implementation detail, stop and propose a precise amendment instead of silently diverging.
