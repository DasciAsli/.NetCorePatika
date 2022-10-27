using FluentValidation;

namespace WebApi.Application.BookOperations.Queries.GetBookById
{
    public class GetBookByIdQueryValidator:AbstractValidator<GetBookByIdQuery>
    {
        public GetBookByIdQueryValidator()
        {
            RuleFor(query=>query.Id).GreaterThan(0);
        }
    }
}