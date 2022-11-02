using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Tests.WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context=testFixture.Context;
            _mapper =testFixture.Mapper;                 
        }

        [Fact]
        public void WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            var author = new Author() {Name="WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn",Surname="WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn",DateOfBirth=DateTime.Now.AddYears(-100)};
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command= new CreateAuthorCommand(_context,_mapper);
            command.Model = new CreateAuthorModel(){Name=author.Name,Surname=author.Surname,DateOfBirth=author.DateOfBirth};

            //act & assert
            FluentActions
            .Invoking(()=>command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar zaten mevcut");

        }

        [Fact]
        public void WhenValidInputAreGiven_Author_ShouldBeCreated()
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(_context,_mapper);
            command.Model = new CreateAuthorModel(){Name="Joanne",Surname="Rowling",DateOfBirth=new DateTime(1965,07,31)};

            //act
            FluentActions.Invoking(()=>command.Handle()).Invoke();

            //assert
            var author= _context.Authors.SingleOrDefault(a=>a.Name==command.Model.Name && a.Surname==command.Model.Surname);
            author.Should().NotBeNull();
            author.Name.Should().Be(command.Model.Name);
            author.Surname.Should().Be(command.Model.Surname);
            author.DateOfBirth.Should().Be(command.Model.DateOfBirth);

        }
       
    }
}