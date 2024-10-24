using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbzProject.ViewModel
{
    public partial class ShowCharacterViewModel : BaseViewModel
    {
        // private JSONServices myServices = new JSONServices();
        public ObservableCollection<DbzCharacter> MyObservableCharacterDbz { get; } = new();
        public ShowCharacterViewModel() 
        {
            loadObservableCharacter();
        }

        public async Task loadObservableCharacter() 
        {
            // recuper le dossier placé dans le bureau 
            String destinationFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "DataDbz\\Image\\");
       
            foreach (var character in Globals.myDbzCharactersFromDb)
            {
                // enlever après 
                if (character.Picture == null)
                {
                    System.Diagnostics.Debug.WriteLine($"Character {character.Name} has null Picture property");
                    continue;
                }

                // je Crée un clone du personnage pour ne pas modifier le chemin de l'image de l'originale 
                DbzCharacter characterClone = new DbzCharacter(character.IdCharacter, character.Name, character.Race, character.Picture, character.Attack, character.UserId);
                // je Modifie le chemin d'image 
                characterClone.Picture = Path.Combine(destinationFolder, Path.GetFileName(character.Picture));
                // Ajouter le clone à la collection observable
                MyObservableCharacterDbz.Add(characterClone);
            }
        }


    }
}
