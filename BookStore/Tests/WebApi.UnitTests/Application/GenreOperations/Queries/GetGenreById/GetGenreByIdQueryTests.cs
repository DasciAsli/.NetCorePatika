using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreById;
using WebApi.DBOperations;

namespace Tests.WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreById
{
    public class GetGenreByIdQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreByIdQueryTests(CommonTestFixture testFicture)
        {
            _context = testFicture.Context;
            _mapper = testFicture.Mapper;
        }

        [Fact]
        public void WhenGivenIdIsNotInDb_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            GetGenreByIdQuery query = new GetGenreByIdQuery(_context, _mapper);
            query.Id = 1800;

            //act & assert
            FluentActions
            .Invoking(() => query.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı");

        }

        [Fact]
        public void WhenGivenIdIsInDb_Genre_ShouldNotBeReturn()
        {
            //arrange
            GetGenreByIdQuery query = new GetGenreByIdQuery(_context, _mapper);
            query.Id = 1;

            //act 
            FluentActions.Invoking(() => query.Handle()).Invoke();

            //assert
            var genre = _context.Genres.SingleOrDefault(g=>g.Id==query.Id);
            genre.Should().NotBeNull();
            

        }
    }

}