using FluentAssertions;
using WebApi.Application.BookOperations.Queries.GetBookById;

namespace Tests.WebApi.UnitTests.Application.BookOperations.Queries.GetBookById
{
    public class GetBookByIdQueryValidatorTests
    {
        [Fact]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError()
        {
            //arrange
            GetBookByIdQuery query = new GetBookByIdQuery(null, null);
            query.Id = -2;

            //act
            GetBookByIdQueryValidator validator = new GetBookByIdQueryValidator();
            var result = validator.Validate(query);

            //assert
            result.Errors.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            GetBookByIdQuery query = new GetBookByIdQuery(null, null);
            query.Id =1;

            //act
            GetBookByIdQueryValidator validator = new GetBookByIdQueryValidator();
            var result = validator.Validate(query);

            //assert
            result.Errors.Count().Should().Be(0);
        }
    }
}