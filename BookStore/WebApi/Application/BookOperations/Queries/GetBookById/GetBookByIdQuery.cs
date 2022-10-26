using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Queries.GetBookById
{
    public class GetBookByIdQuery
    {
        public int Id { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetBookByIdQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public GetBookByIdModel Handle()
        {
            var book = _dbContext.Books.Include(x=>x.Genre).Where(x => x.Id == Id).SingleOrDefault();

            if (book != null)
            {
                GetBookByIdModel vm = _mapper.Map<GetBookByIdModel>(book);//book'u GetBookByIdModel'e dönüştür
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