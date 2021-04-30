using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HrManager.ValidationRules
{
  public static class ValidationHelper
  {
    /// <summary>
    /// Sert à valider un objet graphique et tous ses descendants
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool IsValid(this DependencyObject obj)
    {
      // The dependency object is valid if it has no errors and all
      // of its children (that are dependency objects) are error-free.

      // Vérifier que l'objet est correct
      return !Validation.GetHasError(obj) &&
          // Obtenir tous les descendants de premier niveau
          LogicalTreeHelper.GetChildren(obj)
          //
          .OfType<DependencyObject>()
          // le All fait la boucle : IsValid doit être vrai pour chacun
          .All(IsValid);
    }
    //public static void PlayUkulele(this Window w)
    //{
    //}
  }
}
