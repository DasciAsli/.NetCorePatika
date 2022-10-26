
using FluentValidation;

namespace WebApi.Application.GenreOperations.Queries.GetGenreById
{
    public class GetGenreByIdQueryValidator :AbstractValidator<GetGenreByIdQuery>
    {
        public GetGenreByIdQueryValidator()
        {
            RuleFor(query => query.Id).GreaterThan(0);
        }      
    }
    
}