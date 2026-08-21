using FastEndpoints;
using FluentValidation;

namespace BackendTakeHome.Modules.Users.Presentation.Users.CreateUser;

public sealed class CreateUserRequestValidator : Validator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(request => request.Username).NotEmpty();
    }
}
