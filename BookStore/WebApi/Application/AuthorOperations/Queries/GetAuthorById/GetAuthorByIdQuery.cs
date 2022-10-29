using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorById
{
    public class GetAuthorByIdQuery
    {
        public int Id { get; set; }
        private readonly IBookStoreDbContext _dbcontext;
        private readonly IMapper _mapper;
        public GetAuthorByIdQuery(IBookStoreDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }
        public GetAuthorByIdModel Handle()
        {
            var author = _dbcontext.Authors.SingleOrDefault(a => a.Id == Id);

            if (author != null)
            {
                GetAuthorByIdModel vm = _mapper.Map<GetAuthorByIdModel>(author);
                return vm;
            }
            else
            {
                throw new InvalidOperationException("Yazar bulunamadı");
            }
        }
    }
    public class GetAuthorByIdModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

}