using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbzProject.ViewModel
{
    // Classe ViewModel pour le menu principal de l'application
    public partial class MainMenuViewModel : BaseViewModel
    {
        // Propriétés observables pour le nom d'utilisateur et les ports COM disponibles
        [ObservableProperty] private string? username;
        [ObservableProperty] private ObservableCollection<string> portsAvailable; // Changé en propriété
        [ObservableProperty] private string? comPortSelectedByUser;

        // Constructeur de la classe
        public MainMenuViewModel()
        {
            // Initialise le nom d'utilisateur et la liste des ports disponibles
            Username = Globals.currentUser.Username;
            portsAvailable = new ObservableCollection<string>();
            DeviceOrientationService.fetchedComPort(portsAvailable);
        }

        // Commande pour rafraîchir le port COM sélectionné
        [RelayCommand]
        public void RefreshComSelected()
        {
            if (ComPortSelectedByUser != null) Globals.comConnected = ComPortSelectedByUser;
        }

        // Commande pour passer à la page d'ajout de personnage
        [RelayCommand]
        private async Task GoToAddFormPageCharacter()
        {
            IsBusy = true;
            await Shell.Current.GoToAsync("FormCharacterPage", true);
            IsBusy = false;
        }

        // Commande pour passer à la page des personnages
        [RelayCommand]
        private async Task GoToCharactersPage()
        {
            IsBusy = true;
            if (IsNotEmptyDataList()) await Shell.Current.GoToAsync("ShowCharacterPage", true);
            else Globals.popUP("No Characters", "Please add characters to your account", "Ok");
            IsBusy = false;
        }

        // Commande pour passer à la page du graphique des personnages
        [RelayCommand]
        private async Task GoToChartCharactersPage()
        {
            IsBusy = true;
            if (IsNotEmptyDataList()) await Shell.Current.GoToAsync("ChartCharactersPage", true);
            else Globals.popUP("No Characters", "Please add characters to your account to view the chart", "Ok");
            IsBusy = false;
        }

        // Commande pour passer à la page d'exportation CSV
        [RelayCommand]
        private async Task GoToExportCSVPage()
        {
            IsBusy = true;
            if (IsNotEmptyDataList()) await Shell.Current.GoToAsync("CharactersCSVExportPage", true);
            else Globals.popUP("No Characters", "Please add characters to your account to export data", "Ok");
            IsBusy = false;
        }

        // Vérifie si la liste de données des personnages n'est pas vide
        private Boolean IsNotEmptyDataList()
        {
            return Globals.myDbzCharactersFromDb.Count > 0;
        }
    }
}
