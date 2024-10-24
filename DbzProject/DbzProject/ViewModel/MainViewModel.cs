using Azure.Identity;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace DbzProject.ViewModel;

public partial class MainViewModel : BaseViewModel
{
    private JSONServices myServices;
    private DataAccessService MyDBService;
    [ObservableProperty]
    private string? username;
    [ObservableProperty]
    private string? password;

    public MainViewModel(DataAccessService myDBService)
    {
        this.MyDBService = myDBService;
        myServices = new JSONServices();
        
    }

    [RelayCommand]
    private async Task GoToMainMenu()
    {
        IsBusy = true;
        if (Username.IsNullOrEmpty() || Password.IsNullOrEmpty()) Globals.popUP("Champs", "Remplisez les champs", "Ok");
        else
        {
            try
            {
                // Recherche de l'utilisateur dans la base de données
                var user = await MyDBService.Users.FirstOrDefaultAsync(u => u.Username == Username && u.Password == Password);
                
                if (user != null)
                {
                   // save les info de l'user localement
                    Globals.currentUser = user;

                    // Récupére tous les personnages associés à cet utilisateur depuis la liste characters de la table user 
                    var characters = await MyDBService.Characters.Where(c => c.UserId == user.IdUser).ToListAsync();
                    Globals.myDbzCharactersFromDb = user.Characters.ToList();

                    
                    myServices.GetDbzCharacter();
       
                    // vide les champs du formulaire
                    Username = String.Empty;
                    Password = String.Empty;

                    // Utilisateur trouvé, rediriger vers MainMenuPage
                    await Shell.Current.GoToAsync("MainMenuPage", true);
                }
                else
                {
                    // Utilisateur non trouvé, afficher un message d'erreur
                    Globals.popUP("Erreur", "Nom d'utilisateur ou mot de passe incorrect", "Ok");
                }
            }
            catch (Exception ex)
            {
                // Gérer les exceptions ici
                Globals.popUP("Erreur", "Une erreur s'est produite : " + ex.Message, "Ok");
            }
        }
        IsBusy = false;
    }

    [RelayCommand]
    private async Task GoToRegisterPage() 
    {
        IsBusy = true;
        await Shell.Current.GoToAsync("RegisterPage", true);
        IsBusy = false;
    }


   
    


}
