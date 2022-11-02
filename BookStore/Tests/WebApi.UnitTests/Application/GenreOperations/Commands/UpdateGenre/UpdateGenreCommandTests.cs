using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;

namespace Tests.WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenIdIsNotInDb_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.Id=1780;

            //act & assert
            FluentActions
            .Invoking(()=>command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı");
     
        }

        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.Id = 2;
            command.Model = new UpdateGenreModel(){Name="Romance"};

            //act & assert
            FluentActions
            .Invoking(()=>command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aynı isimli bir kitap türü zaten mevcut");
            
        }
  
        [Fact]
        public void WhenGivenIdIsInDb_Genre_ShouldBeUpdated()
        {
            //arrange
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.Id=1;
            command.Model=new UpdateGenreModel(){Name="Travel",IsActive=false};

            //act
            FluentActions.Invoking(()=>command.Handle()).Invoke();

            //assert
            var genre = _context.Genres.SingleOrDefault(g=>g.Id==command.Id);
            genre.Should().NotBeNull();
            genre.Name.Should().Be(command.Model.Name);
            genre.IsActive.Should().Be(command.Model.IsActive);

        }

    }    
}