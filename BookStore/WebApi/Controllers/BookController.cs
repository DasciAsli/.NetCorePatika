using System.Threading.Tasks.Dataflow;
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
        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }

        //GET İŞLEMLERİ
        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        //ROUTE İLE GET
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetBookByIdQuery query = new GetBookByIdQuery(_context);
            try
            {
                query.Id = id;
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
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
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
            UpdateBookCommand command = new UpdateBookCommand(_context);
            try
            {
                command.Model = updatedBook;
                command.Id = id;
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
                command.Id=id;
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