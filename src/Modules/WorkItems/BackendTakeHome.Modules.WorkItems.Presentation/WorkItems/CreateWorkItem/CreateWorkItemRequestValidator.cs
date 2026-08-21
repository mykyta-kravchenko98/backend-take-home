using FastEndpoints;
using FluentValidation;

namespace BackendTakeHome.Modules.WorkItems.Presentation.WorkItems.CreateWorkItem;

public sealed class CreateWorkItemRequestValidator : Validator<CreateWorkItemRequest>
{
    public CreateWorkItemRequestValidator()
    {
        RuleFor(request => request.Name).NotEmpty();
        RuleFor(request => request.AssigneeId).NotEmpty();
    }
}
