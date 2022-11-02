using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;

namespace Tests.WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-3)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id)
        {
            //arrange
            DeleteBookCommand command= new DeleteBookCommand(null);
            command.Id=id;

            //act
            DeleteBookCommandValidator validator= new DeleteBookCommandValidator();
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
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.Id=id;

            //act
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);

        }

    }
}