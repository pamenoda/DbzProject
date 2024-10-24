using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbzProject.ViewModel
{
    public partial class RegisterViewModel : BaseViewModel
    {
        // regex conforme 
        private const string UsernameRegexPattern = "^[a-zA-Z][a-zA-Z0-9]{2,8}[a-zA-Z0-9]$";
        private const string PasswordRegexPattern = @"^[A-Z][a-zA-Z]*\d{1,}$";

        private const string UsernameDetails = "Pour un nom d'utilisateur correct, les caractéristiques sont :\r\n\r\n" +
    "- Peut contenir des lettres (majuscules ou minuscules), des chiffres et le caractère souligné _.\r\n" +
    "- Doit commencer par une lettre majuscule ou minuscule.\r\n" +
    "- Doit avoir une longueur comprise entre 4 et 8 caractères.\r\n\r\n" +
    "Exemple de nom d'utilisateur correct : aBcd1234.";

        private const string PasswordDetails = "Pour un mot de passe correct, les caractéristiques sont :\r\n\r\n" +
    "- Doit commencer par une lettre majuscule.\r\n" +
    "- Peut contenir des lettres (majuscules ou minuscules), des chiffres.\r\n" +
    "- Doit avoir une longueur minimale de 6 caractères.\r\n" +
    "- Doit se terminer par un ou plusieurs chiffres.\r\n\r\n" +
    "Exemple de mot de passe correct : Password123.";

        [ObservableProperty] private string? username;
        [ObservableProperty] private string? password;
        [ObservableProperty] private string? confirmPassword;
        DataAccessService MyDBService;

        public RegisterViewModel(DataAccessService myDBService)
        {
            this.MyDBService = myDBService;
        }

        [RelayCommand]
        private async Task saveInDb()
        {
            IsBusy = true;
            if (Password.IsNullOrEmpty() || Username.IsNullOrEmpty() || ConfirmPassword.IsNullOrEmpty())
            {
                Globals.popUP("Champs", "remplissez les champs", "Ok");
            }
            else if(!Regex.IsMatch(Username, UsernameRegexPattern))
            {
                Globals.popUP("Nom d'utilisateur", UsernameDetails, "Ok");
            }
            else if (!Regex.IsMatch(Password, PasswordRegexPattern))
            {
                Globals.popUP("Mot de passe", PasswordDetails, "Ok");
            }
            else if (Password != ConfirmPassword)
            {
                Globals.popUP("Password", "Mot de passe diffèrent", "Ok");
            }
            else
            {
                // Crée un nouvel utilisateur avec les données saisies
                // (Guid.NewGuid génére un identifiant unique)
                User newUser = new User(Guid.NewGuid().ToString(), Username, Password);

                try
                {
                    await MyDBService.Users.AddAsync(newUser);
                    await MyDBService.SaveChangesAsync();

                    Globals.popUP("Succès", "Utilisateur enregistré avec succès\n Vous pouvez vous connecter dès à présent ", "Ok");

                }
                catch (Exception ex)
                {
                    Globals.popUP("Erreur", "Une erreur s'est produite lors de l'enregistrement de l'utilisateur : " + ex.Message, "Ok");
                }

                await Shell.Current.GoToAsync("..", true);
            }
           
            IsBusy = false;
        }

    }
}
