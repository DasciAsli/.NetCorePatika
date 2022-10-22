using System.Threading.Tasks.Dataflow;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private static List<Book> BookList = new List<Book>(){
            new Book{Id=1,Title="Lean Startup",GenreId=1,PageCount=200,PublishDate=new DateTime(2001,06,12)},
            new Book{Id=2,Title="Herland",GenreId=2,PageCount=250,PublishDate=new DateTime(2010,05,23)},
            new Book{Id=3,Title="Dune",GenreId=2,PageCount=540,PublishDate=new DateTime(2002,12,21)}
        };



        //GET İŞLEMLERİ
        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList=BookList.OrderBy(x=>x.Id).ToList<Book>();
            return bookList;
        }

        //Route ile Get
        [HttpGet("{id}")]
        public Object GetById(int id)
        {
            var book=BookList.Where(x=>x.Id==id).SingleOrDefault();
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
        //     var book=BookList.Where(book =>book.Id==(Convert.ToInt32(id))).SingleOrDefault();
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

            var book = BookList.SingleOrDefault(book => book.Title==newBook.Title);
            if (book != null)
            {
                return BadRequest();
            }
            else
            {
                BookList.Add(newBook);
                return Ok();
            }

        }


        //PUT İŞLEMİ
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] Book updatedBook){

            var book = BookList.SingleOrDefault(book => book.Id==id);
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
                return Ok();
            }

        }
         
         //DELETE İŞLEMİ
         [HttpDelete("{id}")]
         public IActionResult DeleteBook(int id)
         {
            var book = BookList.SingleOrDefault(book =>book.Id==id);
            if (book == null)
            {
                return BadRequest();
            }
            else
            {
                BookList.Remove(book);
                return Ok();
            }
         }
    }
}