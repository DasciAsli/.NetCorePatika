using AutoMapper;
using WebApi.DBOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {
        public string RefreshToken{get; set;}
        private readonly IBookStoreDbContext _dbcontext;
        private readonly IConfiguration _configuration;

        public RefreshTokenCommand(IBookStoreDbContext dbcontext, IConfiguration configuration)
        {
            _dbcontext = dbcontext;      
            _configuration = configuration;
        }
        public Token Handle()
        {
            var user = _dbcontext.Users.FirstOrDefault(u=>u.RefreshToken==RefreshToken && u.RefreshTokenExpireDate > DateTime.Now);
            if (user is not null)
            {
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);//Bu 5dklık süre içerisinde refreshtokenla gidip accesstoken alabilecek
                _dbcontext.SaveChanges();
                return token;
            }
            else
            {
                throw new InvalidOperationException("Valid bir Refresh Token bulunamadı");
            }

        } 

    }

}
