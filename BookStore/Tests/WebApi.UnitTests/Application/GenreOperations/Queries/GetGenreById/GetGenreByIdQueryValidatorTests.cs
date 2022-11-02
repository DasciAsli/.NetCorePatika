using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreById;

namespace Tests.WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreById
{
    public class GetGenreByIdQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id)
        {
            //arrange
            GetGenreByIdQuery query = new GetGenreByIdQuery(null, null);
            query.Id = id;

            //act
            GetGenreByIdQueryValidator validator = new GetGenreByIdQueryValidator();
            var result = validator.Validate(query);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError(int id)
        {
            //arrange
            GetGenreByIdQuery query = new GetGenreByIdQuery(null, null);
            query.Id = id;

            //act
            GetGenreByIdQueryValidator validator = new GetGenreByIdQueryValidator();
            var result = validator.Validate(query);

            //assert
            result.Errors.Count.Should().Be(0);

        }

    }
}