using System.Threading.Tasks.Dataflow;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        private readonly IBookStoreDbContext _dbcontext;
        private readonly IMapper _mapper;
        public CreateBookCommand(IBookStoreDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var book = _dbcontext.Books.SingleOrDefault(book => book.Title == Model.Title);
            if (book != null)
            {
                throw new InvalidOperationException("Kitap zaten mevcut");
            }
            // if (!(_dbcontext.Authors.Any(a => a.Id == Model.AuthorId)))
            // {
            //     throw new InvalidOperationException("Yazar bulunamadığı için kitap eklenemedi");
            // }
            // if (!(_dbcontext.Genres.Any(g => g.Id == Model.GenreId)))
            // {
            //     throw new InvalidOperationException("Kitap türü bulunamadığı için kitap eklenemedi");
            // }

            book = _mapper.Map<Book>(Model); //Model ile gelen veriyi Book objesine convert et.MappingProfile sınıfından faydalandı
            _dbcontext.Books.Add(book);
            _dbcontext.SaveChanges();

        }


    }

    public class CreateBookModel //Bir kitap ekleneceği zaman kullanıcıdan hangi verilerin girilmesini isteriz
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}