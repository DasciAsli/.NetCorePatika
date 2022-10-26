using AutoMapper;
using FluentValidation.Validators;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.Queries.GetBookById;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreById;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Entities;

namespace WebApi.Common
{
    public class MappingProfile :Profile //Profile sınıfından kalıtım aldırdıktan sonra bu automapper tarafından bir config dosyası olarak gözükecek 
    {
        public MappingProfile()
        {
            //Book
            CreateMap<Book,BooksViewModel>().ForMember(dest =>dest.Genre,opt=>opt.MapFrom(src=>src.Genre.Name));
            CreateMap<Book,GetBookByIdModel>().ForMember(dest =>dest.Genre,opt=>opt.MapFrom(src=>src.Genre.Name));//Genreyi maplerken neye göre maplemesi gerektiğini belirtmiş olduk
            CreateMap<CreateBookModel,Book>();//CreateBookModel objesi Book objesine maplenebilir olsun.
            CreateMap<UpdateBookModel,Book>();

            //Genre
            CreateMap<Genre,GenresViewModel>(); //Genreyı GenresViewModele dönüştür.
            CreateMap<Genre,GetGenreByIdModel>();
            CreateMap<CreateGenreModel,Genre>();
        }
    }
}