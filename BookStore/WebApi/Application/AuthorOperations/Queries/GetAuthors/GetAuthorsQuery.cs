using System.ComponentModel;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        public AuthorsViewModel Model { get; set; }
        private readonly BookStoreDbContext _dbcontext;
        private readonly IMapper _mapper;
        public GetAuthorsQuery(BookStoreDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }
        public List<AuthorsViewModel> Handle()
        {
            var authors=_dbcontext.Authors.OrderBy(a=>a.Id).ToList();
            List<AuthorsViewModel> vm = _mapper.Map<List<AuthorsViewModel>>(authors);
            return vm;           
        }
    }
    public class AuthorsViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}