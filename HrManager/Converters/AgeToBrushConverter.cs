using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace HrManager.Converters
{
  public class AgeToBrushConverter : IValueConverter
  {
    public Brush MinorBrush { get; set; } = new SolidColorBrush(Color.FromRgb(128, 0, 0));
    public Brush AdultBrush { get; set; } = new SolidColorBrush(Color.FromRgb(0, 128, 0));
    // Equivalent à :
    //private Brush adultBrush = new SolidColorBrush(Color.FromRgb(0, 128, 0));
    //public Brush AdultBrush
    //{
    //  get { return adultBrush; }
    //  set { adultBrush = value; }
    //}
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      // Méthode appelée lorsque l'IHM requiert les données pour les afficher
      int age = (int)value;
      if (age >= 18)
        return AdultBrush;
      else
        return MinorBrush;

    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      // Méthode appelée lorsque l'IHM doit convertir les données saisies par l'utilisateur pour
      // les remettre dans le modèle : (presque) jamais utile
      throw new NotImplementedException();
    }
  }
}
