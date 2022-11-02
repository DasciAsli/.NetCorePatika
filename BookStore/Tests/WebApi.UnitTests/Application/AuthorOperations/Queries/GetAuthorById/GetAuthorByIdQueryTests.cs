using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.AuthorOperations.Queries.GetAuthorById;
using WebApi.DBOperations;

namespace Tests.WebApi.UnitTests.Application.AuthorOperations.Queries.GetGenreById
{
    public class GetAuthorByIdQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorByIdQueryTests(CommonTestFixture testFicture)
        {
            _context = testFicture.Context;
            _mapper = testFicture.Mapper;
        }

        [Fact]
        public void WhenGivenIdIsNotInDb_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            GetAuthorByIdQuery query = new GetAuthorByIdQuery(_context, _mapper);
            query.Id = 1800;

            //act & assert
            FluentActions
            .Invoking(() => query.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar bulunamadı");

        }

        [Fact]
        public void WhenGivenIdIsInDb_Author_ShouldNotBeReturn()
        {
            //arrange
            GetAuthorByIdQuery query = new GetAuthorByIdQuery(_context, _mapper);
            query.Id = 3;

            //act 
            FluentActions.Invoking(() => query.Handle()).Invoke();

            //assert
            var author = _context.Authors.SingleOrDefault(a => a.Id == query.Id);
            author.Should().NotBeNull();


        }

    }

}