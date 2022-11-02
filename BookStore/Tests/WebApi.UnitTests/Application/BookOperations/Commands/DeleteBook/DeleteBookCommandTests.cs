using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;

namespace Tests.WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;

        }

        [Fact]
        public void WhenGivenBookIdIsNotinDB_InvalidOperationException_ShouldBeReturn()//Verilen kitapidsi dbde bulunamadığında
        {
            //arrange(Hazırlık)
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.Id=0;

            //act(Çalıştırma) & assert(Doğrulama)
            FluentActions
            .Invoking(()=>command.Handle())//Handle metodu çalıştırıldığında
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadi");           

        }

        [Fact]
        public void WhenGivenBookIdIsinDB_Book_ShouldBeDeleted()
        {
            //arrange(Hazırlık)
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.Id=1;

            //act(Çalıştırma) 
            FluentActions
            .Invoking(()=>command.Handle()).Invoke();
            
            
            //assert(Doğrulama)
            var book = _context.Books.SingleOrDefault(b=>b.Id==command.Id);
            book.Should().Be(null);
        }

    }
}