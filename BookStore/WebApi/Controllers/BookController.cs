using System.Threading.Tasks.Dataflow;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        public BookController(BookStoreDbContext context)
        {
            _context=context;
        }
     
        //GET İŞLEMLERİ
        [HttpGet]
        public List<Book> GetBooks()
        {
            
            var bookList = _context.Books.OrderBy(x=>x.Id).ToList<Book>();
            return bookList;
        }

        //Route ile Get
        [HttpGet("{id}")]
        public Object GetById(int id)
        {
            var book=_context.Books.Where(x=>x.Id==id).SingleOrDefault();
            if (book != null)
            {
            return book;
            }
            else
            {
                return "Kitap bulunamadı";
            }
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

        //POST İŞLEMİ
        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook){

            var book = _context.Books.SingleOrDefault(book => book.Title==newBook.Title);
            if (book != null)
            {
                return BadRequest();
            }
            else
            {
                _context.Books.Add(newBook);
                _context.SaveChanges();
                return Ok();
            }

        }

        //PUT İŞLEMİ
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] Book updatedBook){

            var book = _context.Books.SingleOrDefault(book => book.Id==id);
            if (book == null)
            {
                return BadRequest();
            }
            else
            {
                book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
                book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
                book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
                book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
                _context.SaveChanges();
                return Ok();
            }

        }
         
         //DELETE İŞLEMİ
         [HttpDelete("{id}")]
         public IActionResult DeleteBook(int id)
         {
            var book = _context.Books.SingleOrDefault(book =>book.Id==id);
            if (book == null)
            {
                return BadRequest();
            }
            else
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
                return Ok();
            }
         }
    }

}