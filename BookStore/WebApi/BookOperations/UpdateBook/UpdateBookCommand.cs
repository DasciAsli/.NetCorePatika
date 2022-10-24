using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        public UpdateBookModel Model { get; set; }
        public int Id { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public UpdateBookCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(book => book.Id==Id);
            if (book == null)
            {
                throw new InvalidOperationException("Kitap bulunamadi");
            }
            else
            {
                book=_mapper.Map(Model,book); //Updatede bu şekilde kullanmak gerekiyor mapi
                _dbContext.SaveChanges();

            }

        }     
    }
    
     public class UpdateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }

        }
}