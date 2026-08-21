# Chat 5 prompt: documentation and final QA

Use `00-shared-context.md` as the preamble. Chats 1–4 must already be complete.

Prepare the repository for submission without expanding product scope. This chat owns the root README, a checked-in `.http` exercise file, AI journey completion, and final verification. It may fix small documentation, formatting, or configuration defects discovered by verification, but must report any behavioral code issue instead of performing an unreviewed redesign.

Required outcomes:

- Root README explains module boundaries, cross-module communication, CQRS flow, persistence choice, trade-offs, and what would change with more time.
- README contains exact restore/build/test/run commands.
- A `.http` file demonstrates the four operations in a usable sequence.
- `ai-journey/toolchain.md` records tools, models, skills, and MCP servers actually used.
- `ai-journey/judgment.md` records where AI helped, where it was wrong/unhelpful, and where a human overrode it. Do not invent experiences; leave explicit placeholders for facts the candidate must supply.
- Run formatting or analyzers already configured by the repository, then run `dotnet build` and `dotnet test`.
- Produce a final submission checklist and disclose any remaining warnings or gaps.

Keep the README short and decision-focused. Do not claim commands passed unless you ran them successfully in the current repository state.

