using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Queries.GetGenreById
{
    public class GetGenreByIdQuery
    {
        public int Id{get; set;}
        private readonly IBookStoreDbContext _dbcontext;
        public readonly IMapper _mapper;
        public GetGenreByIdQuery(IBookStoreDbContext dbcontext,IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }
        public GetGenreByIdModel Handle()
        {
            var genre=_dbcontext.Genres.SingleOrDefault(g=>g.IsActive==true && g.Id==Id);
            if (genre == null)
            {
                throw new InvalidOperationException("Kitap türü bulunamadı");
            }
            GetGenreByIdModel vm = _mapper.Map<GetGenreByIdModel>(genre);
            return vm;
        }
    }

    public class GetGenreByIdModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}