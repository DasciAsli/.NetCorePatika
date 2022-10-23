using WebApi.DBOperations;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        public UpdateBookModel Model { get; set; }
        public int Id { get; set; }
        private BookStoreDbContext _dbContext;
        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext =dbContext;
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
                book.Title = Model.Title != default ? Model.Title : book.Title;
                book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
                book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
                book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;
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