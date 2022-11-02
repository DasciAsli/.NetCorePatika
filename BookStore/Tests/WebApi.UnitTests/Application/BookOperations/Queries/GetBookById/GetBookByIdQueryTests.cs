using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.BookOperations.Queries.GetBookById;
using WebApi.DBOperations;

namespace Tests.WebApi.UnitTests.Application.BookOperations.Queries.GetBookById
{
    public class GetBookByIdQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBookByIdQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGivenIdIsNotInDb_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            GetBookByIdQuery query = new GetBookByIdQuery(_context, _mapper);
            query.Id = 0;

            //act & assert
            FluentActions
            .Invoking(() => query.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadı");
        }

        [Fact]
        public void WhenGivenIdIsInDb_Book_ShouldNotBeReturn()
        {
            //arrange
            GetBookByIdQuery query = new GetBookByIdQuery(_context, _mapper);
            query.Id = 2;

            //act 
            FluentActions.Invoking(() => query.Handle()).Invoke();

            //assert
            var book = _context.Books.SingleOrDefault(b=>b.Id==query.Id);
            book.Should().NotBeNull();
        }

    }
}