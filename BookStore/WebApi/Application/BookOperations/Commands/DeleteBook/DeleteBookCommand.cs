using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommand
    {
        public int Id { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        public DeleteBookCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(book => book.Id == Id);
            if (book == null)
            {
                throw new InvalidOperationException("Kitap bulunamadi");
            }
            else
            {
                _dbContext.Books.Remove(book);
                _dbContext.SaveChanges();
            }

        }

    }
}