using Dapper;
using HrManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

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
        string sqlEmployeeInsert = "INSERT INTO dbo.Employees (FirstName, LastName, Age) Values (@FirstName, @LastName, @Age);";
        int affectedRows = connection.Execute(sqlEmployeeInsert, person);
      }
    }
    internal static List<Person> GetAll()
    {
      using (IDbConnection connection = new SqlConnection(ConnectionString))
      {
        string sqlEmployeeSelect = "Select FirstName, LastName, Age from dbo.Employees";
        // Ce n'est plus la méthode Execute mais Query pour lire des données
        // La méthode "mappe" les colonnes sur des propriétés d'objets de type Person
        // Les Person sont instanciées par Dapper
        IEnumerable<Person> cursor = connection.Query<Person>(sqlEmployeeSelect);
        return  cursor.ToList();
      }
    }
  }
}
