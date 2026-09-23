using FluentValidation;

namespace TimewaysAPI.Application.Events.Validators;

public sealed class UpdateEventRequestValidator
    : AbstractValidator<UpdateEventRequest>
{
    public UpdateEventRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .MaximumLength(2000);

        RuleFor(x => x.EndAt)
            .GreaterThan(x => x.StartAt)
            .WithMessage("EndAt must be after StartAt.");

        RuleFor(x => x.Location)
            .MaximumLength(500);
    }
}