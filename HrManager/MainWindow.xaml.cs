using HrManager.ViewModel;
using System.Windows;

namespace HrManager
{
  /// <summary>
  /// Logique d'interaction pour MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    /// Bonne pratique de mettre les ViewModel en donnée membre 
    /// pour pouvoir y accéder dans toutes les callbacks
    private MainViewModel viewModel;
    public MainWindow()
    {
      InitializeComponent();
      viewModel = new MainViewModel();
      DataContext = viewModel;
    }

    private void ButtonHireClick(object sender, RoutedEventArgs e)
    {
      // 1) IHM : Afficher la boîte de dialogue et obtenir l'employé
      // Slide 66
      WindowAddEmployee window = new WindowAddEmployee();
      // affichage modal : bloquante
      //valorisation du retour par prop. DialogResult
      bool? result = window.ShowDialog();
      
      // Comment sait on que l'utilisateur a appuyé sur OK ?
      if(result == true)
      {
        // 2) ViewModel/Data : appeler une méthode du VM HireEmployee
        viewModel.HireEmployee(window.Person);
      }
    }
  }
}
