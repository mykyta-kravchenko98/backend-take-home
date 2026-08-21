# Chat 1 prompt: solution foundation

Use `00-shared-context.md` as the preamble.

Create the buildable layered solution foundation described in ADR-0001. This chat owns solution/project scaffolding, the API host, minimal Common Domain/Application primitives, empty module registration seams, central package/version configuration if useful, and baseline test project scaffolding.

Required outcomes:

- A `.sln` and all projects named in the ADR exist with correct references.
- The API uses .NET 8 and FastEndpoints and can start.
- Common.Domain contains only minimal Result/Error primitives; Common.Application contains MediatR-backed command/query contracts.
- Users.Contracts provides the public assignee-validation contract and nothing module-internal.
- Each module has the Domain/Application/Infrastructure/Presentation projects defined in ADR-0001 and a compilation-safe registration entry point.
- `dotnet build` succeeds and at least a trivial host smoke test can be added if it is reliable.

Do not implement the four feature use cases, domain entities, or feature endpoints. Do not write the final README. Do not add mechanisms excluded by ADR-0001. Keep interfaces minimal; later chats should not have to delete speculative abstractions.
