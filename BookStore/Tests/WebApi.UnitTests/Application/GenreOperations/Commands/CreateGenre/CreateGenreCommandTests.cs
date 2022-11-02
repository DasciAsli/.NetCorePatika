using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Tests.WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        
        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            var genre = new Genre() { Name="WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn" };
            _context.Genres.Add(genre);
            _context.SaveChanges();


            CreateGenreCommand command= new CreateGenreCommand(_context,_mapper);
            command.Model=new CreateGenreModel(){Name=genre.Name};

            //act & assert
            FluentActions
            .Invoking(()=>command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü zaten mevcut");
           
        }

        [Fact]
        public void WhenValidInputIsGiven_Genre_ShouldBeCreated()
        {
            //arrange
            CreateGenreCommand command= new CreateGenreCommand(_context,_mapper);
            command.Model=new CreateGenreModel(){Name="Detective"};
   
            //act
            FluentActions
            .Invoking(()=>command.Handle()).Invoke();
        
            //assert
            var genre=_context.Genres.SingleOrDefault(g=>g.Name==command.Model.Name);
            genre.Should().NotBeNull();
            genre.Name.Should().Be(command.Model.Name);
        }


    }
}