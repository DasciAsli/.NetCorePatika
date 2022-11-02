using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DBOperations;

namespace Tests.WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGivenIdIsNotInDb_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context, _mapper);
            command.Id = 1500;

            //act & assert
            FluentActions
            .Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar bulunamadı");

        }

        [Fact]
        public void WhenGivenIdIsInDb_Author_ShouldBeUpdated()
        {
            //arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context, _mapper);
            command.Model = new UpdateAuthorModel() { Name = "WhenGivenIdIsInDb_Book_ShouldBeUpdated", Surname = "WhenGivenIdIsInDb_Book_ShouldBeUpdated", DateOfBirth = DateTime.Now.AddYears(-56) };
            command.Id = 3;

            //act
            FluentActions
            .Invoking(() => command.Handle()).Invoke();

            //assert
            var author = _context.Authors.SingleOrDefault(a => a.Id == command.Id);
            author.Should().NotBeNull();
            author.Name.Should().Be(command.Model.Name);
            author.Surname.Should().Be(command.Model.Surname);
            author.DateOfBirth.Should().Be(command.Model.DateOfBirth);

        }

    }
}