# Chat 3 prompt: WorkItems module

Use `00-shared-context.md` as the preamble. Chat 1 must already be complete.

Implement the WorkItems module end to end across its Domain, Application, Infrastructure, Presentation, and unit-test projects. Depend only on Users.Contracts for assignee existence validation. You may make a small composition edit to the API host only if the existing module registration seam requires it.

Required outcomes:

- A DDD-oriented `WorkItem` entity enforces ID, name, and assignee ID invariants.
- A module-local repository interface and thread-safe in-memory adapter are implemented.
- `CreateWorkItemCommand` validates the assignee through the public Users contract and returns a clear missing-user result.
- `GetWorkItemsByAssigneeQuery` returns stable API projections and does not access Users internals.
- FastEndpoints in Presentation send MediatR commands/queries and implement `POST /api/work-items` and `GET /api/work-items?assigneeId={userId}`.
- Add focused tests for domain invariants, successful creation, missing assignee, and list filtering.
- Relevant build/tests pass.

Do not modify Users internals or access its repository. Do not duplicate a fake Users repository in production code. In unit tests, use a small test double for the public existence-checking contract. Do not add domain events, Unit of Work, EF Core, or pipeline behaviors.
