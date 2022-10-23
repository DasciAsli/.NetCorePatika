using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBookById
{
    public class GetBookByIdQuery
    {
        public int Id { get; set; }
        private BookStoreDbContext _dbContext;
        public GetBookByIdQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public GetBookByIdModel Handle()
        {
            var book = _dbContext.Books.Where(x => x.Id == Id).SingleOrDefault();

            if (book != null)
            {
                GetBookByIdModel vm = new GetBookByIdModel();
                vm.Title = book.Title;
                vm.PageCount = book.PageCount;
                vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
                vm.Genre = ((GenreEnum)book.GenreId).ToString();
                return vm;
            }
            else
            {
                throw new InvalidOperationException("Kitap bulunamadı");
            }

        }
     
    }
    
    public class GetBookByIdModel
        {
            public string Title { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
            public string Genre { get; set; }
        }
}