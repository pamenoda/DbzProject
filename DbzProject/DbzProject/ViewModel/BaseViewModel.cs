using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbzProject.ViewModel
{
    // Classe de base pour les ViewModels, héritant de ObservableObject
    public abstract partial class BaseViewModel : ObservableObject
    {
        // Propriété privée pour stocker l'état d'occupation du ViewModel
        [ObservableProperty] // Indique à ObservableObject de surveiller les changements de cette propriété
        [NotifyPropertyChangedFor(nameof(IsNotBusy))] // Notifie les vues lorsque IsNotBusy change
        bool isBusy;

        // Propriété calculée pour indiquer si le ViewModel n'est pas occupé
        public bool IsNotBusy => !IsBusy;
    }
}
