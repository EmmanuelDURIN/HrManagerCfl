﻿using System;
using System.Collections.ObjectModel;
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

    public bool IsRefreshing
    {
      get { return isRefreshing; }
      set
      {
        // 1) Emission évt que IsRefreshing a changé
        bool hasChanged = SetProperty(ref isRefreshing, value);
        if (hasChanged)
        {
          // 2) On émet un évt pour dire au bouton de se mettre à jour
          RefreshCmd.FireExecuteChanged();
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
    // Propriété liée, calculée, dérivée. 
    // Pas de donnée en propre, pas de champ
    public bool IsNotRefreshing
    {
      get { return !isRefreshing; }
    }
    public RelayCommand RefreshCmd { get; set; }
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

      // Refresh au démarrage de l'application
      //Refresh(null);
    }
    private bool CanRefresh(object obj)
    {
      return !IsRefreshing;
    }

    private async void Refresh(object o)
    {
      // !!! utiliser la propriété pour émettre l'évenement
      IsRefreshing = true;
      people.Clear();

      // Pas bien bloque le thread graphique :
      //Thread.Sleep(5000);

      //Bien : car bloque l'exécution des instructions suivantes
      // libère le thread graphique pour qu'il puisse exécuter d'autres callback
      await Task.Delay(5000);

      // Pour éxécuter du code sur un autre thread : calcul,...
      //Task.Run();

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
      IsRefreshing = false;
    }
  }
}
