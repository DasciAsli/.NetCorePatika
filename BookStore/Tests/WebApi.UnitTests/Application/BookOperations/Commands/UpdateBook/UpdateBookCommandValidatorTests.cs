using FluentAssertions;
using WebApi.Application.BookOperations.Commands.UpdateBook;

namespace Tests.WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests
    {
        [Theory]
        [InlineData(1, "A", 150, 2, 3)]//Title
        [InlineData(1, "AAA", -10, 2, 3)]//PageCount
        [InlineData(1, "AAA", 150, -2, 3)]//AuthorId
        [InlineData(1, "AAA", 150, 2, -3)]//GenreId
        [InlineData(-2, "A", -10, -2, -2)]//All
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id, string title, int pageCount, int authorId, int genreId)
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(null, null);
            command.Id = id;
            UpdateBookModel model = new UpdateBookModel();
            model.Title = title;
            model.PageCount = pageCount;
            model.AuthorId = authorId;
            model.GenreId = genreId;
            model.PublishDate = DateTime.Now.AddYears(-5);
            command.Model = model;

            //act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count().Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1, "AAAA",150,2,3)]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors(int id, string title, int pageCount, int authorId, int genreId)
        {
            UpdateBookCommand command = new UpdateBookCommand(null, null);
            command.Id = id;
            UpdateBookModel model = new UpdateBookModel();
            model.Title = title;
            model.PageCount = pageCount;
            model.AuthorId = authorId;
            model.GenreId = genreId;
            model.PublishDate = DateTime.Now.AddYears(-5);
            command.Model = model;

            //act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count().Should().Be(0);

        }
        
    }   
}