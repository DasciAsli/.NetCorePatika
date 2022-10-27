using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public int Id { get; set; }
        public UpdateAuthorModel Model;
        private readonly BookStoreDbContext _dbcontext;
        private readonly IMapper _mapper;
        public UpdateAuthorCommand(BookStoreDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }
        public void Handle()
        {
            var author = _dbcontext.Authors.SingleOrDefault(a=>a.Id==Id);
            if (author == null)
            {
                throw new InvalidOperationException("Yazar bulunamadı");
            }
            if (_dbcontext.Authors.Any(g=>g.Name.ToLower()==Model.Name.ToLower() && g.Surname.ToLower() == Model.Surname.ToLower() && g.Id!= Id))
            {
                throw new InvalidOperationException("Yazar zaten mevcut");
            }
            author =_mapper.Map(Model,author);
            _dbcontext.SaveChanges();
        }
    }
    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}