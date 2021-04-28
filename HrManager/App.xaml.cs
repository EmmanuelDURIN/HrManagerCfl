using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HrManager
{
  /// <summary>
  /// Logique d'interaction pour App.xaml
  /// </summary>
  public partial class App : Application
  {
    public App()
    {
      InitializeComponent();
      this.DispatcherUnhandledException += App_DispatcherUnhandledException;
    }

    private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
      // C'est pas grave, on continue quand même
      e.Handled = true;
      // Bonne pratique : brancher un système de log (Serilog, log4net plus simple et plus vieux) et logger les exceptions
      //log.Error();
    }
  }
}
