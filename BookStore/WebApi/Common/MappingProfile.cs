using AutoMapper;
using FluentValidation.Validators;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorById;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.Queries.GetBookById;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreById;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.UserOperations.Commands.CreateUser;
using WebApi.Entities;

namespace WebApi.Common
{
    public class MappingProfile :Profile //Profile sınıfından kalıtım aldırdıktan sonra bu automapper tarafından bir config dosyası olarak gözükecek 
    {
        public MappingProfile()
        {
            //Book
            CreateMap<Book,BooksViewModel>().ForMember(dest =>dest.Genre,opt=>opt.MapFrom(src=>src.Genre.Name)).ForMember(dest => dest.Author,opt => opt.MapFrom(src => src.Author.Name +" "+ src.Author.Surname));
            CreateMap<Book,GetBookByIdModel>().ForMember(dest =>dest.Genre,opt=>opt.MapFrom(src=>src.Genre.Name)).ForMember(dest => dest.Author,opt => opt.MapFrom(src => src.Author.Name +" "+ src.Author.Surname));//Genreyi maplerken neye göre maplemesi gerektiğini belirtmiş olduk
            CreateMap<CreateBookModel,Book>();//CreateBookModel objesi Book objesine maplenebilir olsun.
            CreateMap<UpdateBookModel,Book>();

            //Genre
            CreateMap<Genre,GenresViewModel>(); //Genreyı GenresViewModele dönüştür.
            CreateMap<Genre,GetGenreByIdModel>();
            CreateMap<CreateGenreModel,Genre>();

            //Author
            CreateMap<Author,AuthorsViewModel>();
            CreateMap<Author,GetAuthorByIdModel>();
            CreateMap<CreateAuthorModel,Author>();
            CreateMap<UpdateAuthorModel,Author>();

            //User
            CreateMap<CreateUserModel,User>();
        }
    }
}