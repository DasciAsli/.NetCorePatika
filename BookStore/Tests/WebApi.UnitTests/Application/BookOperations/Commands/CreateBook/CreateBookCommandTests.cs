using System;
using System.Reflection;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Tests.WebApi.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture> //CommandTestFixture sınıfını tanıtabilmek için bunu yapıyoruz.Xunitten gelior
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }


        [Fact] //Bunun bir test metodu olduğunu belirtir.
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()//Zaten var olan bir kitapın title'ı verildiğinde InvalidOperationException hatasını geri döndürmeli
        {
            //arrange(Hazırlık)
            var book = new Book() { Title = "Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn", GenreId = 1, AuthorId = 1, PageCount = 100, PublishDate = new DateTime(1990, 01, 10) };
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookModel() { Title = book.Title };


            //act(Çalıştırma) & assert(Doğrulama)
            FluentActions
             .Invoking(() => command.Handle()) //Handle metodu çalıştırıldığında
             .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut");

        }

        [Fact]
        public void WhenValidInputAreGiven_Book_ShouldBeCreated()
        {

            //arrange
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            CreateBookModel model = new CreateBookModel() { Title = "Hobbit",GenreId = 1, AuthorId = 1, PageCount = 1000, PublishDate = DateTime.Now.AddYears(-10) };
            command.Model=model;

            //act(Çalıştırma)
            FluentActions
             .Invoking(() => command.Handle()).Invoke();

            //assert
            var book = _context.Books.SingleOrDefault(book=>book.Title== model.Title);


            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);
            book.AuthorId.Should().Be(model.AuthorId);



        }

    }

}