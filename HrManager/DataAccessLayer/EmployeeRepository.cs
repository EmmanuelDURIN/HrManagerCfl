using Dapper;
using HrManager.ViewModel;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HrManager.DataAccessLayer
{
  // Cette classe est un Repository (ou Data Access object)
  // Son but est de prendre en charge les lectures/écritures sur la base de données
  public class EmployeeRepository
  {
    private const string ConnectionString = @"Data Source=(localdb)\MSSQLLOCALDB;Initial Catalog=HrManager;Integrated Security=True;Pooling=False";

    // insert en db
    internal static void Add(Person person)
    {
      using (IDbConnection connection = new SqlConnection(ConnectionString))
      {
        string sqlEmployeeInsert = "INSERT INTO dbo.Employees (FirstName, LastName, Age) Values (@Firstname, @LastName, @Age);";
        // customers = db.Query<Customer>("Select * From Customers").ToList();
        int affectedRows = connection.Execute(sqlEmployeeInsert, person );
      }
    }
  }
}
