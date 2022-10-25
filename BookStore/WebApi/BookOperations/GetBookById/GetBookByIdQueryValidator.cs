using FluentValidation;

namespace WebApi.BookOperations.GetBookById
{
    public class GetBookByIdQueryValidator:AbstractValidator<GetBookByIdQuery>
    {
        public GetBookByIdQueryValidator()
        {
            RuleFor(query=>query.Id).GreaterThan(0);        
        }
    }
}