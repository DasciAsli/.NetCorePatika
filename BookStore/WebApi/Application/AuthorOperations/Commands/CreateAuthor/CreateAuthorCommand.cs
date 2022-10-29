using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public CreateAuthorModel Model;
        private readonly IBookStoreDbContext _dbcontext;
        private readonly IMapper _mapper;
        public CreateAuthorCommand(IBookStoreDbContext dbcontext,IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _dbcontext.Authors.SingleOrDefault(a=>a.Name==Model.Name && a.Surname==Model.Surname);
            if (author != null)
            {
                throw new InvalidOperationException("Yazar zaten mevcut");
            }
            author = _mapper.Map<Author>(Model);
            _dbcontext.Authors.Add(author);
            _dbcontext.SaveChanges();
        }
    }
    public class CreateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

}