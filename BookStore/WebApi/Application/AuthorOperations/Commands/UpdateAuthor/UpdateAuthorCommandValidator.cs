using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(a => a.Id).GreaterThan(0);
            RuleFor(a => a.Model.Name).NotEmpty().MinimumLength(2);
            RuleFor(a => a.Model.Surname).NotEmpty().MinimumLength(2);
            RuleFor(a => a.Model.DateOfBirth.Date).NotEmpty().LessThan(DateTime.Now.Date);
        }
    }
}