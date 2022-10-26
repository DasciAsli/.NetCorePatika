using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public UpdateGenreModel Model;
        public int Id { get; set; }
        private readonly BookStoreDbContext _dbcontext;
        public UpdateGenreCommand(BookStoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public void Handle()
        {
            var genre = _dbcontext.Genres.SingleOrDefault(g=>g.Id==Id);
            if (genre == null)
            {
                throw new InvalidOperationException("Kitap türü bulunamadı");
            }
            if(_dbcontext.Genres.Any(g=>g.Name.ToLower()==Model.Name.ToLower() && g.Id!= Id))
            {
                throw new InvalidOperationException("Aynı isimli bir kitap türü zaten mevcut");
            }
            genre.Name =string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name:Model.Name ;
            genre.IsActive = Model.IsActive;
            _dbcontext.SaveChanges();
        }
    }
    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }=true;
    }
}