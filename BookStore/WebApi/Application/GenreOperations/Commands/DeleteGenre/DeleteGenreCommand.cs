using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int Id { get; set; }
        private readonly BookStoreDbContext _dbcontext;
        public DeleteGenreCommand(BookStoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public void Handle()
        {
            var genre = _dbcontext.Genres.SingleOrDefault(g => g.Id == Id);
            if (genre == null)
            {
                throw new InvalidOperationException("Kitap türü bulunamadı");
            }
            else
            {
                _dbcontext.Remove(genre);
                _dbcontext.SaveChanges();
            }
        }
    }

}