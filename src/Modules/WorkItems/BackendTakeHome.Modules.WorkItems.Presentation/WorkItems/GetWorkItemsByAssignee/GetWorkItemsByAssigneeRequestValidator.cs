using FastEndpoints;
using FluentValidation;

namespace BackendTakeHome.Modules.WorkItems.Presentation.WorkItems.GetWorkItemsByAssignee;

public sealed class GetWorkItemsByAssigneeRequestValidator : Validator<GetWorkItemsByAssigneeRequest>
{
    public GetWorkItemsByAssigneeRequestValidator()
    {
        RuleFor(request => request.AssigneeId).NotEmpty();
    }
}
