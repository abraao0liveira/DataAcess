using Dapper;
using DataAcess.Models;
using Microsoft.Data.SqlClient;

namespace DataAcess;

class Program
{
    static void Main(string[] args)
    {
        const string connectionString =
            "Server=localhost;Database=study_db;User Id=sa;Password=C3rul3@nC@v3_150;Trusted_Connection=False;MultipleActiveResultSets=true;TrustServerCertificate=true;";

        using (var connection = new SqlConnection(connectionString))
        {
            //UpdateCategory(connection);
            //CreateCategory(connection);
            //DeleteCategory(connection);
            CreateManyCategory(connection);
            ListCategories(connection);
        }
    }

    //methods
    static void ListCategories(SqlConnection connection)
    {
        const string sqlSelect = " select [id], [title] from [category] ";
        var categories = connection.Query<Category>(sqlSelect);

        foreach (var item in categories)
        {
            Console.WriteLine($"{item.Id} - {item.Title}");
        }
    }
    
    static void CreateCategory(SqlConnection connection)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Title = "Amazon AWS",
            Url = "https://aws.amazon.com",
            Summary = "AWS Cloud",
            Order = 8,
            Description = "Categoria de cursos de aws",
            Featured = true
        };

        const string sqlInsert = " insert into [category] " +
                                 " values(@Id, @Title, @Url, @Summary, @Order, @Description, @Featured) ";

        var rows = connection.Execute(sqlInsert, new
        {
            Id = category.Id,
            Title = category.Title,
            Url = category.Url,
            Summary = category.Summary,
            Order = category.Order,
            Description = category.Description,
            Featured = category.Featured
        });
        Console.WriteLine($"Inserted {rows} rows");
    }
    
    static void UpdateCategory(SqlConnection connection)
    {
        const string sqlUpdate = " update [category] set [title] = @Title where [id] = @Id";
        var rows = connection.Execute(sqlUpdate, new
        {
            Id = new Guid("100708e9-6b66-4f2d-8c24-af447dbcf1b2"),
            Title = "Frontend"
        });
        Console.WriteLine($"Updated {rows} rows");
    }
    
    static void CreateManyCategory(SqlConnection connection)
    {
        var categoryOne = new Category
        {
            Id = Guid.NewGuid(),
            Title = "Amazon AWS",
            Url = "https://aws.amazon.com",
            Summary = "AWS Cloud",
            Order = 8,
            Description = "Categoria de cursos de aws",
            Featured = true
        };
        
        var categoryTwo = new Category
        {
            Id = Guid.NewGuid(),
            Title = "Nova Categoria",
            Url = "https://new.category",
            Summary = "Nova Categoria",
            Order = 9,
            Description = "Categoria de cursos",
            Featured = false
        };

        const string sqlInsert = " insert into [category] " +
                                 " values(@Id, @Title, @Url, @Summary, @Order, @Description, @Featured) ";

        var rows = connection.Execute(sqlInsert, new[]{
            new
            {
                Id = categoryOne.Id,
                Title = categoryOne.Title,
                Url = categoryOne.Url,
                Summary = categoryOne.Summary,
                Order = categoryOne.Order,
                Description = categoryOne.Description,
                Featured = categoryOne.Featured
            },
            new
            {
                Id = categoryTwo.Id,
                Title = categoryTwo.Title,
                Url = categoryTwo.Url,
                Summary = categoryTwo.Summary,
                Order = categoryTwo.Order,
                Description = categoryTwo.Description,
                Featured = categoryTwo.Featured
            }
        });
        Console.WriteLine($"Inserted {rows} rows");
    }
    
    static void DeleteCategory(SqlConnection connection)
    {
        var sqlDelete = " delete from [category] where [id] = @Id ";

        var rows = connection.Execute(sqlDelete, new
        {
            id = new Guid("")
        });
        Console.WriteLine($"Deleted {rows} rows");
    }
}
