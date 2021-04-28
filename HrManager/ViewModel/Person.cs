using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace HrManager.ViewModel
{
  public class Person : BindableBase
  {
    private int age;

    public int Age
    {
      get { return age; }
      set
      {
        SetProperty(ref age, value);
      }
    }
    private string firstName;

    public string FirstName
    {
      get { return firstName; }
      set
      {
        SetProperty(ref firstName, value);
      }
    }

    private string lastName;

    public string LastName
    {
      get { return lastName; }
      set
      {
        SetProperty(ref lastName, value);
      }
    }
    // Pour le fun, pour le débogagen, pas le mieux, 
    // on préferera un ItemTemplate
    public override string ToString()
    {
      return $"{FirstName,-15} {LastName,-15} {Age,4}";
    }

    //// le compilo génère un champ : int age
    ////public int Age { get; set; }

    //// équivalent

    //// champ, private en général
    //private int age;

    //// Propriété
    //public int Age
    //{
    //  get { return age; }
    //  set {
    //    SetProperty(ref age, value);
    //  //  age = value; 
    //  }
    //}
  }
}
