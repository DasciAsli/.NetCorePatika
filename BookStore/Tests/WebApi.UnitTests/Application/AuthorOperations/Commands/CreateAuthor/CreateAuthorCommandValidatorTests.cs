using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;

namespace Tests.WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests :IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("A"," Orwell")]
        [InlineData("George","B")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name,string surname)
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null,null);
            CreateAuthorModel model = new CreateAuthorModel(){Name=name,Surname=surname,DateOfBirth=DateTime.Now.AddYears(-119)};
            command.Model=model;

            //act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Theory]
        [InlineData("George"," Orwell")]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError(string name,string surname)
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null,null);
            CreateAuthorModel model = new CreateAuthorModel(){Name=name,Surname=surname,DateOfBirth=DateTime.Now.AddYears(-119)};
            command.Model=model;

            //act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);

        }
        
    }
}