using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model;
        private readonly IBookStoreDbContext _dbcontext;
        public readonly IMapper _mapper;
        public CreateGenreCommand(IBookStoreDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var genre =_dbcontext.Genres.SingleOrDefault(g=>g.Name==Model.Name);
            if (genre != null)
            {
                throw new InvalidOperationException("Kitap türü zaten mevcut");
            }
            genre = _mapper.Map<Genre>(Model);
            _dbcontext.Genres.Add(genre);
            _dbcontext.SaveChanges(); 
        }
    }
    public class CreateGenreModel
    {
        public string Name { get; set; }
    }
}