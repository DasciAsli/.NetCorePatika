using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApi.Entities;
using WebApi.TokenOperations.Models;

namespace WebApi.TokenOperations
{
    public class TokenHandler
    {
        private readonly IConfiguration Configuration;//appsetting.jsondaki verileri okuyabilmek için gerekiyor.
        public TokenHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Token CreateAccessToken(User user)//User'a göre geriye Token dönen bir metot
        {
            Token tokenModel = new Token();
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));//Program.csde belirlediğimiz key
            SigningCredentials signingCredentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);//Şifrelenmiş kimlik
            tokenModel.Expiration = DateTime.Now.AddMinutes(15);//15dklık bir accesstoken oluşturduk

            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer:Configuration["Token:Issuer"],
                audience:Configuration["Token:Audience"],
                expires:tokenModel.Expiration,
                notBefore:DateTime.Now, //Token üretildikten ne kdr süre sonra kullanılmaya başlasın
                signingCredentials:signingCredentials
                
            );
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            //Token yaratılıyor.
            tokenModel.AccessToken=tokenHandler.WriteToken(securityToken);
            tokenModel.RefreshToken =CreateRefreshToken();
            return tokenModel;
        }
        
        public string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }


    }
}