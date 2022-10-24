using AutoMapper;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBookById;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;

namespace WebApi.Common
{
    public class MappingProfile :Profile //Profile sınıfından kalıtım aldırdıktan sonra bu automapper tarafından bir config dosyası olarak gözükecek 
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel,Book>();//CreateBookModel objesi Book objesine maplenebilir olsun.
            CreateMap<Book,GetBookByIdModel>().ForMember(dest =>dest.Genre,opt=>opt.MapFrom(src=>((GenreEnum)src.GenreId).ToString()));//Genreyi maplerken neye göre maplemesi gerektiğini belirtmiş olduk
            CreateMap<Book,BooksViewModel>().ForMember(dest =>dest.Genre,opt=>opt.MapFrom(src=>((GenreEnum)src.GenreId).ToString()));
            CreateMap<UpdateBookModel,Book>(); 
        }
    }
}