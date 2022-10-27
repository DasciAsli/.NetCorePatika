using FluentValidation;

namespace WebApi.Application.AuthOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidator:AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorCommandValidator()
        {
            RuleFor(a=>a.Id).GreaterThan(0);           
        }       
    }
}