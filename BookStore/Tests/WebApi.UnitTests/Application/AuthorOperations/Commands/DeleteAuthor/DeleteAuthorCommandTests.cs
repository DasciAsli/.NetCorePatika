using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DBOperations;

namespace Tests.WebApi.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenAuthorIdIsNotinDB_InvalidOperationException_ShouldBeReturn()
        {
            //arrange(Hazırlık)
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.Id = 0;

            //act(Çalıştırma) & assert(Doğrulama)
            FluentActions
            .Invoking(() => command.Handle())//Handle metodu çalıştırıldığında
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar bulunamadı");

        }

        [Fact]
        public void WhenGivenAuthorIdIsinDB_Author_ShouldBeDeleted()
        {
            //arrange(Hazırlık)           
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.Id = 4;

            //act(Çalıştırma)
            FluentActions
            .Invoking(() => command.Handle()).Invoke();


            //assert(Doğrulama)
            var author = _context.Authors.SingleOrDefault(a => a.Id == command.Id);
            author.Should().Be(null);
        }


    }
}