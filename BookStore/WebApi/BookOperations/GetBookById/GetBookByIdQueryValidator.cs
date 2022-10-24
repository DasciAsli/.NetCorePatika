using FluentValidation;

namespace WebApi.BookOperations.GetBookById
{
    public class GetBookByIdQueryValidator:AbstractValidator<GetBookByIdQuery>
    {
        public GetBookByIdQueryValidator()
        {
            RuleFor(command=>command.Id).GreaterThan(0);        
        }
    }
}