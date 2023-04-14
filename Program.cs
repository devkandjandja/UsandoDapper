using System.Data;
using ConexaoDapper;
using Dapper;
using Microsoft.Data.SqlClient;

const string conectionString = "server=localhost,1433; database=balta; User ID=sa; Password=1q2w3e4r@#$;  trust server certificate=true";

using(var connection = new SqlConnection(conectionString))
{   
    //ListCategories(connection); 
    //CreateCategory(connection);
    //UpDateCategory(connection);
    //DeleteCategory(connection);
    //CreateManyCategory(connection);
     ExecuteProcedure(connection);
}

static void ListCategories(SqlConnection connection)
{
  var categories = connection.Query<Category>("select [Id], [Title] from [Category]");
   foreach(var item in categories)
   {
        Console.WriteLine($"{item.Id} -{item.Title}");
   }
}
static void CreateCategory(SqlConnection connection)
{
    var category = new Category();
        category.Id = Guid.NewGuid();
        category.Title = "Amazon AWS";
        category.Url = "amazon";
        category.Summary = "AWS Cloud";
        category.Order = 8;
        category.Description = "Categoria destinada a serviços do AWS";
        category.Featured = false;
    var insertSql = "insert into [Category] values(@Id, @Title, @Url, @Summary, @Order, @Description, @Featured)";

    var rows = connection.Execute(insertSql, new {
            category.Id,
            category.Title,
            category.Url,
            category.Summary,
            category.Order,
            category.Description,
            category.Featured
        });

        Console.WriteLine($"{rows} linhas afetadas.");
}
static void UpDateCategory(SqlConnection connection)
{
    var updateQuery = "update [Category] set [Title] = @title where [Id] = @id";
    var rows = connection.Execute(updateQuery, new 
    {
        id ="",
        title = "FrontEnd 2022"
    });

    Console.WriteLine($"{rows} linhas atualizadas");
}
static void DeleteCategory(SqlConnection connection)
{
    var deleteQuery = "delete [Category] where [Id] = @id";
    var rows = connection.Execute(deleteQuery, new
    {
    
    });
}
static void CreateManyCategory(SqlConnection connection)
{
    var  category = new Category();
    category.Id = Guid.NewGuid();
    category.Title = "Amazon Cloud  ";
    category.Url = "amazon";
    category.Summary = "AWS Cloud";
    category.Order = 10;
    category.Description = "Categoria destinada a serviços do AWS";
    category.Featured = false;
 
    var category2 = new Category();
    category2.Id = Guid.NewGuid();
    category2.Title = "Categoria nova";
    category2.Url = "nova-categoria";
    category2.Summary = "Categoria";
    category2.Order = 11;
    category2.Description = "Categoria destinada a serviços do novos";
    category2.Featured = true;
var insertSql = "insert into [Category] values(@Id, @Title, @Url, @Summary, @Order, @Description, @Featured)";

  var rows = connection.Execute(insertSql, new []
  {
        new 
        {
            category.Id,
            category.Title,
            category.Url,
            category.Summary,
            category.Order,
            category.Description,
            category.Featured
        },

        new 
        {
            category2.Id,
            category2.Title,
            category2.Url,
            category2.Summary,
            category2.Order,
            category2.Description,
            category2.Featured
        }
     });

    Console.WriteLine($"{rows} linhas afetadas.");
}

static void ExecuteProcedure(SqlConnection connection)
{
    var procedure = "[spDeleteStudent]";
    var pars = new {StudentId = "956b55a8-de8b-42d7-91e1-d715fefe9350"};
    var  affectedRows = connection.Execute(procedure, pars, commandType: CommandType.StoredProcedure);

    Console.WriteLine($"{affectedRows} linhas afetadas");
}