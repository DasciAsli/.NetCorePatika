using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.CreateGenre;

namespace Tests.WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests
    {
        [Theory]
        [InlineData("A")]
        [InlineData("")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
        {
            //arrange
            CreateGenreCommand command = new CreateGenreCommand(null, null);
            command.Model = new CreateGenreModel() { Name = name };

            //act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count().Should().BeGreaterThan(0);

        }

        [Theory]
        [InlineData("AA")]
        [InlineData("AAA")]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError(string name)
        {
            //arrange
            CreateGenreCommand command = new CreateGenreCommand(null, null);
            command.Model = new CreateGenreModel() { Name = name };

            //act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count().Should().Be(0);
        }
        
    }

}