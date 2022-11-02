using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;

namespace Tests.WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0,"AA")] //id
        [InlineData(2,"A")]  //name
        [InlineData(3," ")]   //name
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id,string name)
        {
            //arrange
            UpdateGenreCommand command= new UpdateGenreCommand(null);
            command.Id=id;
            command.Model=new UpdateGenreModel(){Name=name};

            //act
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result=validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Theory]
        [InlineData(1,"Poem")]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors(int id,string name)
        {
            //arrange
            UpdateGenreCommand command= new UpdateGenreCommand(null);
            command.Id=id;
            command.Model=new UpdateGenreModel(){Name=name};

            //act
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result=validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);

        }


    }
}