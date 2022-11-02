using System.Runtime.CompilerServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

namespace Tests.WebApi.UnitTests.TestSetup
{
    public class CommonTestFixture //Bana context ve mapper'ı verecek olan sınıf
    {
        public BookStoreDbContext Context {get; set;}
        public IMapper Mapper { get; set; }

        public CommonTestFixture()
        {
            //CONTEXT
            var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(databaseName:"BookStoreTestDB").Options;
            //Tek bir BookStoreDbContext objemden onun optionsını test projem içerisinden değiştirip yaratmış oldum
            Context = new BookStoreDbContext(options);
            Context.Database.EnsureCreated();//Oluşturulduğundan emin olmak istiyorum contextin           
            Context.AddGenres();
            Context.AddAuthors();
            Context.AddBooks();
            Context.SaveChanges();

            //MAPPER
            //Mapping ayarlarını yapıyoruz.Bu ayarı yaparken WebApinin içerisindeki MappingProfile ayarlarını baz alarak bunu yapmasını söylüyoruz.
            Mapper = new MapperConfiguration(cfg =>{cfg.AddProfile<MappingProfile>();}).CreateMapper();

        }

    }
}