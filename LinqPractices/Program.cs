using System.Net;
using LinqPractices.DBOperations;
using LinqPractices.Entities;

internal class Program
{
    private static void Main(string[] args)
    {
        DataGenerator.Initialize();
        LinqDbContext _context = new LinqDbContext();

        //ToList(Tüm öğrencilerin adlarını listele)
        ToListQuery(_context);

        //Where(Students tablosundan İd'si 1 olan öğrenciyi getir)
        WhereQuery(_context);

        //Find(Students tablosundan İd'si 1 olan öğrenciyi getir)
        FindQuery(_context);

        //FirstOrDefault(Students tablosundan ilk id'deki öğrenciyi getir)
        //First ile FirstOrDefault'un farkı Firstte aradığı elemanı bulamazsa hata fırlatır.FirstOrDefaultta ise null döndürür
        FirstOrDefaultQuery(_context);

        //SingleOrDefault(Students tablosundan adı Deniz olan öğrenciyi getir)
        //SingleOrDefault bir tane sonuç dönmesini bekler eğer birden fazla veri dönecekse hata fırlatır.
        SingleOrDeafultQuery(_context);

        //OrderBy(Öğrencileri ClassIdlerine göre sıralayarak isimlerini yazdır)
        //OrderByDescending(Tersten sıralamak için kullanılıyor.)
        OrderByQuery(_context);

        //Anonymous Object Result(LINQ her zaman geriye entity objesi dönmek zorunda değildir.Query sonucunu kendi yarattığınız bir obje formatında döndürebilirsiniz.)
        AnonymousObjResult (_context);

        

        static void WhereQuery(LinqDbContext _context)
        {
            Console.WriteLine("*****Where*****");
            var student = _context.Students.Where(student => student.Id == 1).SingleOrDefault();
            if (student is not null)
            {
                Console.WriteLine("Name : {0} \n", student.Name);
            }
        }

        static void FindQuery(LinqDbContext _context)
        {
            System.Console.WriteLine("*****Find*****");
            var student = _context.Students.Find(1);
            if (student is not null)
            {
                Console.WriteLine("Name : {0} \n", student.Name);
            }
        }

        static void FirstOrDefaultQuery(LinqDbContext _context)
        {
            System.Console.WriteLine("*****FirstOrDefault*****");
            var student = _context.Students.FirstOrDefault();
            if (student is not null)
            {
                Console.WriteLine("Name : {0} \n", student.Name);
            }
        }

        static void SingleOrDeafultQuery(LinqDbContext _context)
        {
            System.Console.WriteLine("*****SingleOrDefault*****");
            var student = _context.Students.SingleOrDefault(student => student.Name == "Deniz");
            if (student is not null)
            {
                Console.WriteLine("Name : {0} \n", student.Name);
            }
        }

        static void ToListQuery(LinqDbContext _context)
        {
            var students = _context.Students.ToList<Student>();
            Console.WriteLine("*****ToList*****");
            if (students is not null)
            {
                foreach (var student in students)
                {
                    Console.WriteLine("Name : {0}", student.Name);
                }
                Console.WriteLine();

            }
        }

        static void OrderByQuery(LinqDbContext _context)
        {
            var students = _context.Students.OrderBy(x => x.ClassId).ToList();
            Console.WriteLine("*****OrderBy*****");
            if (students is not null)
            {
                foreach (var student in students)
                {
                    Console.WriteLine("Name : {0}", student.Name);
                }
                Console.WriteLine();

            }
        }

        static void AnonymousObjResult(LinqDbContext _context)
        {
            var anonymousObjResults = _context.Students.Where(x => x.ClassId == 2).Select(x => new { Name = x.Name, SurName = x.Surname });
            Console.WriteLine("*****Anonymous Object Result*****");
            if (anonymousObjResults is not null)
            {
                foreach (var student in anonymousObjResults)
                {
                    Console.WriteLine("Name : {0}", student.Name);
                }
                Console.WriteLine();

            }
        }
    }

}