using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ViewModel;
using ViewModel.Commands;

namespace HrManager.ViewModel
{
  public class MainViewModel : BindableBase
  {
    private ObservableCollection<Person> people = new ObservableCollection<Person>();
    private Person currentPerson;
    private bool isRefreshing;
    // le CancellationTokenSource implémente la partie micro(source) du  pattern de l'observateur(source/écouteur)
    // 
    private CancellationTokenSource cancellationTokenSource;

    public bool IsRefreshing
    {
      get { return isRefreshing; }
      set
      {
        // 1) Emission évt que IsRefreshing a changé
        bool hasChanged = SetProperty(ref isRefreshing, value);
        if (hasChanged)
        {
          // 2) On émet un évt pour dire aux boutons de se mettre à jour
          RefreshCmd.FireExecuteChanged();
          CancelCmd.FireExecuteChanged();
          // 3) On émet un évt pour dire à la Grid de se mettre à jour
          // Notifier tous les composants bindés sur IsNotRefreshing qu'il faut se mettre à jour
          //OnPropertyChanged("IsNotRefreshing");
          // Mieux : car avec nameof, 
          // a) le compilo vérifie
          // b) les renommages sont effectifs
          OnPropertyChanged(nameof(IsNotRefreshing));
        }
      }
    }

    public void HireEmployee(Person person)
    {
      people.Add(person);
    }


    // Propriété liée, calculée, dérivée. 
    // Pas de donnée en propre, pas de champ
    public bool IsNotRefreshing
    {
      get { return !isRefreshing; }
    }
    public RelayCommand RefreshCmd { get; set; }
    public RelayCommand CancelCmd { get; set; }
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
      // on donne Refresh et CanRefresh en callback sur le bouton
      RefreshCmd = new RelayCommand(Refresh, CanRefresh);
      CancelCmd = new RelayCommand(Cancel, CanCancel);
      // Refresh au démarrage de l'application
      //Refresh(null);
    }
    private void Cancel(object obj)
    {
      if (cancellationTokenSource != null)
        // on émet la demande d'annulation dans le micro
        cancellationTokenSource.Cancel();
    }
    private bool CanCancel(object obj)
    {
      return isRefreshing;
    }
    private bool CanRefresh(object obj)
    {
      return !IsRefreshing;
    }
    private async void Refresh(object o)
    {
      cancellationTokenSource = new CancellationTokenSource();
      // le CancellationToken est l'écouteur du pattern Observateur
      CancellationToken cancellationToken = cancellationTokenSource.Token;
      // !!! utiliser la propriété pour émettre l'évenement
      IsRefreshing = true;
      people.Clear();

      try
      {
        // Pas bien bloque le thread graphique :
        //Thread.Sleep(5000);

        //Bien : car bloque l'exécution des instructions suivantes
        // libère le thread graphique pour qu'il puisse exécuter d'autres callback
        // En .Net, on a de nombreuses fonctions asynchrones qui acceptent un CancellationToken -
        // les tâches peuvent ainsi être arrêtées
        await Task.Delay(5000, cancellationToken);
        //string importantData = await ReadData(cancellationToken);
        // Pour éxécuter du code sur un autre thread : calcul,...
        //Task.Run(...);

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
      catch (TaskCanceledException)
      {
        // Normal : l'utilisateur a appuyé sur Cancel
        // Ne pas rémettre l'exception
        //throw;
      }
      finally
      {
        // Pour être sur que IsRefreshing soit mis à false, 
        // même en cas d'exception pas attrapée
        IsRefreshing = false;
      }
    }
    // Les fonctions async ne peuvent avoir que 3 types de retour
    // 1) Task<T> si on a qq chose à retourner (équivalent à retourner T) - bien
    // 2) Task si on a rien à retourner (équivalent à void) - bien
    // 3) void -  moins bien car exceptions mal propagées - mais bien si on implémente une callback appelée par WPF
    // car Wpf se fiche des exceptions
    private async Task<string> ReadData(CancellationToken cancellationToken)
    {
      // using nous assure que le fichier est fermé sur l'accolade fermante
      // quel que soit la fin ( normale, return,exception)
      using (Stream sr = new FileStream("data.txt", FileMode.Open))
      {
        byte[] buffer = new byte[1024];
        // on peut passer le cancellationToken à la fonction
        Task<int> task = sr.ReadAsync(buffer, 0, 1024, cancellationToken);
        // await
        // 1) nous fait attendre la fin de la lecture
        // 2) déballe l'entier de la tâche
        // 3) n'est utilisable que dans les fonctions async
        // 4) libère le thread graphique
        int nbRead = await task;
        return System.Text.Encoding.Default.GetString(buffer);
      } // sr.Close();
    }
  }
}
