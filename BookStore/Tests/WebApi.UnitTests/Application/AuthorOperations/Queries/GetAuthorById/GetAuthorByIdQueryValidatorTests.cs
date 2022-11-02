using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.AuthorOperations.Queries.GetAuthorById;

namespace Tests.WebApi.UnitTests.Application.AuthorOperations.Queries.GetGenreById
{
    public class GetAuthorByIdQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id)
        {
            //arrange
            GetAuthorByIdQuery query = new GetAuthorByIdQuery(null, null);
            query.Id = id;

            //act
            GetAuthorByIdQueryValidator validator = new GetAuthorByIdQueryValidator();
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
            GetAuthorByIdQuery query = new GetAuthorByIdQuery(null, null);
            query.Id = id;

            //act
            GetAuthorByIdQueryValidator validator = new GetAuthorByIdQueryValidator();
            var result = validator.Validate(query);

            //assert
            result.Errors.Count.Should().Be(0);

        }

    }
}