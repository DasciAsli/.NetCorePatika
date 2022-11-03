using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommand
    {
        public CreateUserModel Model;
        private readonly IBookStoreDbContext _dbcontext;
        public readonly IMapper _mapper;
        public CreateUserCommand(IBookStoreDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var user = _dbcontext.Users.SingleOrDefault(u => u.Email == Model.Email);
            if (user != null)
            {
                throw new InvalidOperationException("Kullanıcı zaten mevcut");
            }
            user = _mapper.Map<User>(Model);
            _dbcontext.Users.Add(user);
            _dbcontext.SaveChanges();
        }
    }
    public class CreateUserModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string  Email { get; set; }
        public string Password { get; set; }
    }


}