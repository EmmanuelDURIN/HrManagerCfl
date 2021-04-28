using System.Collections.ObjectModel;
using System.Linq;
using ViewModel;

namespace HrManager.ViewModel
{
  public class MainViewModel : BindableBase
  {
    private ObservableCollection<Person> people = new ObservableCollection<Person>();
    private Person currentPerson;

    public ObservableCollection<Person> People
    {
      get { return people; }
      set
      {
        SetProperty(ref people, value);
      }
    }
    public Person CurrentPerson
    {
      get { return currentPerson; }
      set
      {
        SetProperty(ref currentPerson, value);
      }
    }

    public MainViewModel()
    {
      Refresh();
    }

    private void Refresh()
    {
      var query = Enumerable.Range(1, 10)
                            .Select(i => new Person
                            {
                              Age = 10 + i,
                              FirstName = "FirstName" + (i + 1),
                              LastName = "LastName" + (i + 1),
                            });
      foreach (var person in query)
      {
        people.Add(person);
      }
    }
  }
}
