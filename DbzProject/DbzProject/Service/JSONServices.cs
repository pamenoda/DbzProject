using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace DbzProject.Service
{
    internal class JSONServices
    {
        string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "DataDbz");
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),"DataDBZ\\dbzChara.json");
        
        internal async Task GetDbzCharacter()
        {
            if (!Directory.Exists(directoryPath) || !File.Exists(filePath))
            {
                Directory.CreateDirectory(directoryPath);
                using FileStream fileStream = File.Create(filePath);
            }
            try
            {
                List<DbzCharacter> characters = new List<DbzCharacter>();
                using var stream = File.Open(filePath, FileMode.Open);
                using var reader = new StreamReader(stream);
                var contents = await reader.ReadToEndAsync();
                if (!string.IsNullOrEmpty(contents)) 
                {
                    characters = JsonSerializer.Deserialize<List<DbzCharacter>>(contents);
                    foreach(DbzCharacter character in characters)
                    {
                        if (character.UserId.Equals(Globals.currentUser.IdUser)) Globals.myCharacter.Add(character);
                    }
                }
                    
                    
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("JSON load Error !   "+directoryPath, ex.Message, "OK");
            }
        }

        internal async Task SetDbzCharacter()
        {
            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                using FileStream fileStream = File.Create(filePath);

                await JsonSerializer.SerializeAsync(fileStream, Globals.myCharacter);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("JSON save Error!", ex.Message, "OK");
            }
        }
    }
}
