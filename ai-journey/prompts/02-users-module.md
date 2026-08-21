# Chat 2 prompt: Users module

Use `00-shared-context.md` as the preamble. Chat 1 must already be complete.

Implement the Users module end to end across its Domain, Application, Infrastructure, Presentation, Contracts, and unit-test projects. You may make a small composition edit to the API host only if the existing module registration seam requires it.

Required outcomes:

- A DDD-oriented `User` entity enforces ID and username invariants.
- A module-local repository interface and thread-safe in-memory adapter are implemented.
- `CreateUserCommand` and `GetUserByIdQuery` have dedicated handlers.
- The Users contract for checking existence is implemented without exposing repository or entity types.
- FastEndpoints in Presentation send MediatR commands/queries and implement `POST /api/users` and `GET /api/users/{id}` with request/response DTOs and correct status behavior.
- Add focused tests for domain invariants and both handlers.
- Relevant build/tests pass.

Do not implement or reference WorkItems. Do not redesign shared CQRS abstractions unless compilation makes a minimal compatible adjustment necessary. Do not expose domain entities directly over HTTP. Do not add domain events, Unit of Work, EF Core, or pipeline behaviors.
