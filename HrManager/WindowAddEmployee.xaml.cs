using HrManager.ValidationRules;
using HrManager.ViewModel;
using System.Windows;

namespace HrManager
{
  /// <summary>
  /// Logique d'interaction pour WindowAddEmployee.xaml
  /// </summary>
  public partial class WindowAddEmployee : Window
  {
    // La personne est exposée sous forme de propriété
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
      // Ici : empêcher la fermeture de la boîte de dialogue si PAS VALIDE
      // ferme la boîte de dialogue
      // Appel Correct et classique d'une méthoide statique
      //if (ValidationHelper.IsValid(this))
      // Comme IsValid est une méthode d'extension
      // elle est appelable (aussi) de la manière suivante:
      if (this.IsValid())
      {
        DialogResult = true;
      }
      //this.PlayUkulele();
      ////ValidationHelper.PlayUkulele(this);
    }

    private void ButtonCancelClick(object sender, RoutedEventArgs e)
    {
      // ferme la boîte de dialogue
      DialogResult = false;
    }
  }
}
