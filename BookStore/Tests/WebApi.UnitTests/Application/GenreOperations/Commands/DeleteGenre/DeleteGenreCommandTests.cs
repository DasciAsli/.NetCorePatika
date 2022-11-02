using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DBOperations;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenGenreIdIsNotinDB_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.Id = 0;

            //act & assert
            FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı");

        }

        [Fact]
        public void WhenGivenGenreIdIsinDB_Genre_ShouldBeDeleted()
        {
            //arrange
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.Id = 3;

            //act 
            FluentActions.Invoking(() => command.Handle()).Invoke();
            
            //assert
            var genre = _context.Genres.SingleOrDefault(g=>g.Id==command.Id);
            genre.Should().Be(null);

        }

    }
}