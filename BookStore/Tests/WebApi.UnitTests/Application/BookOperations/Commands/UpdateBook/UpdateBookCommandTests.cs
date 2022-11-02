using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;

namespace Tests.WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateBookCommandTests(CommonTestFixture testFixture) 
        {
            _context=testFixture.Context;
            _mapper=testFixture.Mapper;           
        }

        [Fact]
        public void WhenGivenIdIsNotInDb_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(_context,_mapper);
            command.Id=1500;

            //act & assert
            FluentActions
            .Invoking(()=>command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadi");

        }

        [Fact]
        public void WhenGivenIdIsInDb_Book_ShouldBeUpdated()
        {
            //arrange
            UpdateBookCommand command= new UpdateBookCommand(_context,_mapper);
            command.Model = new UpdateBookModel(){Title="WhenGivenIdIsInDb_Book_ShouldBeUpdated",AuthorId=2,GenreId=2,IsActive=true,PageCount=150,PublishDate=DateTime.Now.AddYears(-10)};
            command.Id=3;

            //act
            FluentActions
            .Invoking(()=>command.Handle()).Invoke();

            //assert
            var book = _context.Books.SingleOrDefault(b=>b.Id==command.Id);
            book.Should().NotBeNull();
            book.Title.Should().Be(command.Model.Title);
            book.AuthorId.Should().Be(command.Model.AuthorId);
            book.GenreId.Should().Be(command.Model.GenreId);
            book.IsActive.Should().Be(command.Model.IsActive);
            book.PageCount.Should().Be(command.Model.PageCount);
            book.PublishDate.Should().Be(command.Model.PublishDate);
        }

    }
}