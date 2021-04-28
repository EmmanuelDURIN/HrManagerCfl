using HrManager.ViewModel;
using System.Windows;

namespace HrManager
{
  /// <summary>
  /// Logique d'interaction pour WindowAddEmployee.xaml
  /// </summary>
  public partial class WindowAddEmployee : Window
  {
    // L apersonne est exposée sous forme de propriété
    // Elle peut ainsi être récupérée par l'appelant de la boîte de dialogue
    // après le clic sur Ok
    public Person Person { get; set; }

    public WindowAddEmployee()
    {
      InitializeComponent();
      Person = new Person
      {
        Age = 10,
        FirstName = "Emmanuel",
        LastName = "Durin",
      };
      // Mettre la personne dans le DataContext pour qu'elle soit liée aux contrôles
      DataContext = Person;
    }

    private void ButtonOkClick(object sender, RoutedEventArgs e)
    {
      // ferme la boîte de dialogue
      DialogResult = true;
    }

    private void Toto(object sender, RoutedEventArgs e)
    {
      // ferme la boîte de dialogue
      DialogResult = false;
    }
  }
}
