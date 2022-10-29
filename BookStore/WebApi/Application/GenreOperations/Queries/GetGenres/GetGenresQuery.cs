using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
       private readonly IBookStoreDbContext _dbcontext;
       public readonly IMapper _mapper;
        public GetGenresQuery(IBookStoreDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public List<GenresViewModel> Handle()
        {
           var genreList= _dbcontext.Genres.Where(g=>g.IsActive==true).OrderBy(g=>g.Id).ToList();
           List<GenresViewModel> vm = _mapper.Map<List<GenresViewModel>>(genreList);        
           return vm;
        }

    }

    public class GenresViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}