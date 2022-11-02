using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;

namespace Tests.WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0,"AAA","AAA")] //id
        [InlineData(2,"A","AAA")]  //name
        [InlineData(3,"AA","A")]   //surname
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id,string name,string surname)
        {
            //arrange
            UpdateAuthorCommand command= new UpdateAuthorCommand(null,null);
            command.Id=id;
            command.Model=new UpdateAuthorModel(){Name=name,Surname=surname,DateOfBirth=DateTime.Now.AddYears(-76)};

            //act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result=validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Theory]
        [InlineData(1,"AAAA","AAAA")]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors(int id,string name,string surname)
        {
            //arrange
            UpdateAuthorCommand command= new UpdateAuthorCommand(null,null);
            command.Id=id;
            command.Model=new UpdateAuthorModel(){Name=name,Surname=surname,DateOfBirth=DateTime.Now.AddYears(-76)};

            //act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result=validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);

        }

        
    }
}