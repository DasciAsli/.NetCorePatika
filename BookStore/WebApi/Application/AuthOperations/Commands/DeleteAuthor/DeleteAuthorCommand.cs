using WebApi.DBOperations;

namespace WebApi.Application.AuthOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int Id { get; set; }
        private readonly BookStoreDbContext _dbcontext;

        public DeleteAuthorCommand(BookStoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public void Handle()
        {
            var author = _dbcontext.Authors.SingleOrDefault(a => a.Id == Id);
            if (author == null)
            {
                throw new InvalidOperationException("Yazar bulunamadı");
            }
            if (_dbcontext.Books.Any(b => b.AuthorId == Id && b.IsActive == true))
            {
                throw new InvalidOperationException("Yazarın yayında olan kitabı mevcut.Lütfen önce kitabı siliniz");
            }
            else
            {
                _dbcontext.Authors.Remove(author);
                _dbcontext.SaveChanges();
            }
        }
    }
}