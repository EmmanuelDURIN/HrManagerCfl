using HrManager.DataAccessLayer;
using HrManager.ViewModel;
using System;
using System.Collections.Generic;

namespace HrManager.BusinessLayer
{
  public class EmployeeManager
  {
    internal static bool Hire(Person person)
    {
      // TODO plus tard : vérifier qu'il n y a pas trop d'employé
      // Vérifier que l'employé est un Top Gun
      if ( IsTopGun( person))
      {
        EmployeeRepository.Add(person);
        return true;
      }
      return false;
    }
    private static bool IsTopGun(Person person)
    {
      return (person.FirstName.Length + person.LastName.Length > 12);
    }

    internal static List<Person> GetAll()
    {
      return EmployeeRepository.GetAll();
    }
  }
}