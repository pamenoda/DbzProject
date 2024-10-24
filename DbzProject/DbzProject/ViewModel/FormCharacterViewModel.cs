using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbzProject.ViewModel
{
    // Classe ViewModel pour le formulaire de création de personnage
    public partial class FormCharacterViewModel : BaseViewModel
    {
        // Propriétés observables pour les champs du formulaire
        [ObservableProperty] private string? id;
        [ObservableProperty] private string? name;
        [ObservableProperty] private string? race;
        [ObservableProperty] private string? attack;
        [ObservableProperty] private string? picture;
        [ObservableProperty] private string? imageTextButton;

        private readonly DeviceOrientationService myScanner;
        private JSONServices myServices;
        private DataAccessService myDBService;

        // Constructeur de la classe
        public FormCharacterViewModel(DataAccessService myDBService)
        {
            // Initialise les services et configure le scanner
            myServices = new JSONServices();
            imageTextButton = "Select an Image";
            this.myDBService = myDBService;
            myScanner = new DeviceOrientationService();
            myScanner.ConfigureScanner(true);
            myScanner.SerialBuffer.Changed += OnBarCodeScanned;
        }

        // Méthode appelée lorsque le scanner lit un code-barres
        private void OnBarCodeScanned(object sender, EventArgs arg)
        {
            try
            {
                DeviceOrientationService.QueueBuffer MyLocalBuffer = (DeviceOrientationService.QueueBuffer)sender;
                Id = MyLocalBuffer.Dequeue().ToString();

            }
            catch (Exception ex)
            {
                // Affiche un message d'erreur si la récupération des données du scanner échoue
                Globals.popUP("Error", "Unable to retrieve scanner data.\nPlease fill in the ID manually.", "Ok");
            }

        }

        // Commande pour fermer le scanner
        [RelayCommand]
        public void CloseScanner()
        {
            myScanner.ConfigureScanner(false);
        }

        // Commande pour enregistrer le personnage
        [RelayCommand]
        private async Task SaveCharacter()
        {
            IsBusy = true;

            // Vérifie que tous les champs sont remplis et valides
            if (string.IsNullOrWhiteSpace(Id) || string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Race) || string.IsNullOrWhiteSpace(Attack) || string.IsNullOrWhiteSpace(Picture))
            {
                Globals.popUP("Error", "All fields are required.", "OK");
            }
            else if (!int.TryParse(Attack, out _))
            {
                Globals.popUP("Error", "Attack must be an integer.", "OK");
            }
            else
            {
                try
                {
                    // Charge à nouveau l'utilisateur depuis la base de données en vérifiant l'ID stocké depuis la connexion
                    var user = await myDBService.Users.FirstOrDefaultAsync(u => u.IdUser == Globals.currentUser.IdUser);

                    // Crée un nouveau personnage avec l'ID généré, et l'ID de l'utilisateur
                    DbzCharacter character = new(Id + ";" + Guid.NewGuid(), Name, Race, Picture, Attack, Globals.currentUser.IdUser);

                    // Ajoute le personnage à la liste JSON
                    Globals.myCharacter.Add(character);
                    await myServices.SetDbzCharacter();

                    // Ajoute le personnage à la liste de la base de données
                    Globals.myDbzCharactersFromDb.Add(character);


                    if (user != null)
                    {
                        // Ajoute le personnage à la collection de personnages de l'utilisateur dans la base de données
                        user.Characters.Add(character);

                        // Met à jour l'entrée de l'utilisateur dans la base de données pour refléter la nouvelle liste de personnages
                        myDBService.Users.Update(user);
                        await myDBService.SaveChangesAsync();

                        // Revenir à la page précédente après l'enregistrement du personnage
                        await Shell.Current.GoToAsync("..", true);
                    }
                    else
                    {
                        Globals.popUP("DB Error", "Account not found.", "Ok");
                    }
                }
                catch (Exception ex)
                {
                    // Gérer les exceptions lors de l'enregistrement du personnage
                    Globals.popUP("Error", "An error occurred while saving the character: " + ex.Message, "OK");
                }
            }

            IsBusy = false;
        }

        // Commande pour sélectionner une image
        [RelayCommand]
        private async Task SelectImage()
        {
            IsBusy = true;
            try
            {
                // Ouvre une boîte de dialogue pour sélectionner une image
                var sourceFilePath = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Please select an image",
                    FileTypes = FilePickerFileType.Images
                });

                if (sourceFilePath == null)
                {
                    return;
                }

                // Récupère le nom du fichier sélectionné et le met à jour dans le bouton
                var fileName = Path.GetFileName(sourceFilePath.FileName);
                ImageTextButton = fileName;

                // Définit le dossier de destination pour enregistrer l'image
                var destinationFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "DataDbz\\Image");
                if (!Directory.Exists(destinationFolder))
                {
                    Directory.CreateDirectory(destinationFolder);
                }

                // Copie l'image dans le dossier de destination
                var destinationFilePath = Path.Combine(destinationFolder, Path.GetFileName(sourceFilePath.FileName));
                using (var sourceStream = await sourceFilePath.OpenReadAsync())
                {
                    using (var destinationStream = File.Create(destinationFilePath))
                    {
                        await sourceStream.CopyToAsync(destinationStream);
                    }
                }
                // Met à jour le chemin de l'image dans le modèle de vue
                Picture = sourceFilePath.FileName;
            }
            catch (Exception ex)
            {
                // Gérer les exceptions lors de la sélection de l'image
            }
            IsBusy = false;
        }
    }
}
