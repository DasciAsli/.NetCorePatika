using System.Threading.Tasks.Dataflow;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookById;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //GET İŞLEMLERİ
        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        //ROUTE İLE GETBYID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetBookByIdQuery query = new GetBookByIdQuery(_context, _mapper);
            try
            {
                query.Id = id;
                GetBookByIdQueryValidator validator = new GetBookByIdQueryValidator();
                validator.ValidateAndThrow(query);
                var result = query.Handle();
                return Ok(result);

            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //POST İŞLEMİ  
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            try
            {
                command.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);//Valide et daha sonra hata mesajını throw et(fırlat).Bunu catchde yakalayıp fırlatacak
                // if (!result.IsValid)
                // {
                //     foreach (var item in result.Errors)
                //     {
                //         System.Console.WriteLine("Özellik " + item.PropertyName + " - Hata mesajı:" + item.ErrorMessage);
                //     }
                // }
                // else
                // {
                //     command.Handle();
                // }
                command.Handle();


            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();

        }

        //PUT İŞLEMİ
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context, _mapper);
            try
            {
                command.Model = updatedBook;
                command.Id = id;
                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();

        }

        //DELETE İŞLEMİ
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            try
            {
                command.Id = id;
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();

            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);

            }
            return Ok();
        }






        //FromQuery ile Get
        // [HttpGet]
        // public Object Get([FromQuery] string id)
        // {
        //     var book=_context.Books.Where(book =>book.Id==(Convert.ToInt32(id))).SingleOrDefault();
        //     if (book != null)
        //     {
        //     return book;
        //     }
        //     else
        //     {
        //         return "Kitap bulunamadı";
        //     } 
        // }
    }

}