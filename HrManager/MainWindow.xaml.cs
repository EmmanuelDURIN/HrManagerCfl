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
  }
}
