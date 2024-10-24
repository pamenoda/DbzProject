using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbzProject.ViewModel
{
    // Classe ViewModel pour gérer l'exportation des personnages au format CSV
    public partial class CharactersCSVViewModel : BaseViewModel
    {
        // Propriétés booléennes pour indiquer si chaque champ est sélectionné pour l'exportation CSV
        [ObservableProperty] // Propriété observée pour mise à jour automatique des vues
        private bool? isNameSelected;
        [ObservableProperty] // Propriété observée pour mise à jour automatique des vues
        private bool? isRaceSelected;
        [ObservableProperty] // Propriété observée pour mise à jour automatique des vues
        private bool? isAttackSelected;

        // Commande pour déclencher l'exportation des données
        public ICommand ExportCommand { get; }

        // Constructeur de la classe
        public CharactersCSVViewModel()
        {
            // Initialise la commande d'exportation avec une fonction asynchrone
            ExportCommand = new Command(async () => await ExportData());
        }

        // Méthode asynchrone pour exporter les données CSV
        private async Task ExportData()
        {
            try
            {
                // Récupère la liste des champs sélectionnés pour l'exportation
                List<string> selectedFields = new List<string>();
                if (IsNameSelected == true) selectedFields.Add("Name");
                if (IsRaceSelected == true) selectedFields.Add("Race");
                if (IsAttackSelected == true) selectedFields.Add("Attack");

                // Génère les en-têtes CSV en fonction des champs sélectionnés
                StringBuilder csvContent = new StringBuilder();
                foreach (string field in selectedFields)
                {
                    csvContent.Append(field + ";");
                }
                csvContent.AppendLine();

                // Ajoute les données pour chaque objet de la collection
                foreach (var character in Globals.myDbzCharactersFromDb)
                {
                    List<string> values = new List<string>();
                    foreach (var field in selectedFields)
                    {
                        // Sélectionne les valeurs des champs correspondants pour chaque personnage
                        switch (field)
                        {
                            case "Name":
                                values.Add(character.Name);
                                break;
                            case "Race":
                                values.Add(character.Race);
                                break;
                            case "Attack":
                                values.Add(character.Attack);
                                break;
                        }
                    }
                    // Ajoute les valeurs des champs pour chaque personnage sur une nouvelle ligne
                    csvContent.AppendLine(string.Join(";", values));
                }

                // Enregistre le contenu CSV dans un fichier
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "DataDbz\\ExportedCharacters.csv");
                await File.WriteAllTextAsync(filePath, csvContent.ToString());

                // Affiche un message de succès
                Globals.popUP("Export CSV", "Data exported successfully to: " + filePath, "OK");

                // Revenir à la page précédente après l'exportation
                await Shell.Current.GoToAsync("..", true);
            }
            catch (IOException ex)
            {
                // Affiche un message d'erreur si le fichier est ouvert par un autre programme
                Globals.popUP("Export CSV", $"Failed to export data. Please make sure the file is not open and try again. Error: {ex.Message}", "OK");
            }
            catch (Exception ex)
            {
                // Gère toutes les autres exceptions imprévues
                Globals.popUP("Export CSV", $"An unexpected error occurred: {ex.Message}", "OK");
            }
        }
    }
}
