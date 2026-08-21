# Chat 4 prompt: integration and API behavior

Use `00-shared-context.md` as the preamble. Chats 2 and 3 must already be complete.

Integrate and verify the completed modules. This chat owns the API composition root and integration test project. It may make narrowly scoped fixes in feature registration or transport mapping, but must not redesign domain models or repositories.

Required outcomes:

- Both modules are registered through their public registration entry points.
- The application starts without manual seed data.
- Integration tests exercise all four required HTTP operations.
- At least one test creates a user and then creates/lists a work item for that user.
- Missing user and invalid input behavior match ADR-0001.
- Tests are isolated and not order-dependent.
- `dotnet build` and `dotnet test` pass from the repository root.

If endpoint behavior and ADR behavior differ, first determine whether this is an implementation defect or an ADR ambiguity. Make only the smallest justified correction and document it in the handoff.
