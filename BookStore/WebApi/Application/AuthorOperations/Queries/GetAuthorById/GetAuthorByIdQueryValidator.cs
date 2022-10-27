using FluentValidation;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorById
{
    public class GetAuthorByIdQueryValidator:AbstractValidator<GetAuthorByIdQuery>
    {
        public GetAuthorByIdQueryValidator()
        {
            RuleFor(a=>a.Id).GreaterThan(0);           
        }
    }
}