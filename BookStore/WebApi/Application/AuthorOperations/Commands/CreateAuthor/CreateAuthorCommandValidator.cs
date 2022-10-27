using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator:AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(a=>a.Model.Name).NotEmpty().MinimumLength(2);
            RuleFor(a=>a.Model.Surname).NotEmpty().MinimumLength(2);
            RuleFor(a => a.Model.DateOfBirth.Date).NotEmpty().LessThan(DateTime.Now.Date);
        }
    }
}