using WebApi.DBOperations;
using WebApi.Entities;

namespace Tests.WebApi.UnitTests.TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(
                new Author { Name = "Eric", Surname = "Ries", DateOfBirth = new DateTime(1978, 09, 22) },
                new Author { Name = "Charlotte", Surname = "Perkins Gilman", DateOfBirth = new DateTime(1860, 07, 03) },
                new Author { Name = "Frank", Surname = "Herbert", DateOfBirth = new DateTime(1920, 10, 09) },
                new Author { Name = "A", Surname = "B", DateOfBirth = new DateTime(1920, 10, 09) }
            );
        }
    }
}