using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;

namespace Tests.WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-3)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id)
        {
            //arrange
            DeleteGenreCommand command= new DeleteGenreCommand(null);
            command.Id=id;

            //act
            DeleteGenreCommandValidator validator= new DeleteGenreCommandValidator();
            var result =validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError(int id)
        {
            //arrange
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.Id=id;

            //act
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);

        }

    }
    
}