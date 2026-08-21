# AI judgment record

## Where AI helped

AI significantly accelerated the repetitive parts of the implementation,
including solution scaffolding, project references, CQRS request and handler
structures, endpoint wiring, and test setup.

Providing the agent with a clear architecture, shared context, and bounded
prompts made the generated changes predictable and easier to review. This
allowed more time to be spent on module boundaries, domain decisions, and final
verification rather than mechanical setup.

## Where AI was wrong or unhelpful

In the initial domain model, AI accepted the entity identifier as a constructor
argument. While externally supplied identifiers can be appropriate when
rehydrating persisted entities, they were unnecessary for the non-rehydrating,
in-memory persistence model used in this exercise.

I changed the entity so that its private constructor generates the identifier
internally with `Guid.NewGuid()`. Callers therefore provide only the data needed
to create the entity and cannot control its identity. This decision was included
in the subsequent prompt, and AI applied it correctly to the second domain
model.

## Human overrides

I deliberately kept persistence in memory instead of introducing EF Core and a
database. For a four-operation take-home exercise, durable persistence,
migrations, and transaction management would add infrastructure without
demonstrating the requested module boundaries more clearly.

I retained control of the Git workflow. AI was allowed to modify and verify the
working tree, but it did not create commits, rewrite history, or push changes.
I reviewed the final diff and decided which changes were suitable for inclusion
in the submission.

Although CI/CD was not required by the exercise, I explicitly asked AI to add a
GitHub Actions workflow. The workflow verifies formatting, restores
dependencies, builds the solution, and runs the automated tests on pushes and
pull requests. This makes the repository checks reproducible outside the local
development environment.

## Final human review

I reviewed the final diff and verified the architectural decisions against the
exercise requirements. I also confirmed that formatting verification, restore,
build, unit tests, integration tests, and the live API smoke flow completed
successfully.

The domain remains intentionally small, but the implementation still includes
explicit invariants, assignee existence checks, expected-error handling, and
compile-time module boundaries. I agree with the documented trade-offs and
remaining scope limitations.