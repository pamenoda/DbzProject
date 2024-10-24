using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbzProject.ViewModel
{
    // Classe ViewModel pour gérer le graphique des personnages
    public partial class ChartCharactersViewModel : BaseViewModel
    {
        // Propriété contenant le graphique observable
        public Chart MyObservableChart { get; set; }

        // Constructeur de la classe
        public ChartCharactersViewModel()
        {
            // Appelle la méthode pour créer le graphique lors de l'initialisation
            CreateChart();
        }

        // Méthode pour créer le graphique
        private void CreateChart()
        {
            // Récupère la liste des personnages depuis la source de données globale
            List<DbzCharacter> characters = Globals.myDbzCharactersFromDb;
            Random random = new Random();

            // Vérifie si la liste des personnages est valide
            if (characters != null)
            {
                // Initialise une liste pour stocker les entrées du graphique
                List<ChartEntry> chartEntries = new List<ChartEntry>();

                // Parcourt chaque personnage pour créer une entrée dans le graphique
                foreach (DbzCharacter character in characters)
                {
                    // Génère une couleur aléatoire pour chaque entrée du graphique
                    SKColor randomColor = new SKColor((byte)random.Next(0, 256), (byte)random.Next(0, 256), (byte)random.Next(0, 256));

                    // Crée une entrée de graphique avec le niveau d'attaque du personnage
                    ChartEntry entry = new ChartEntry(float.Parse(character.Attack))
                    {
                        Label = character.Name, // Nom du personnage
                        ValueLabel = character.Attack, // Valeur de l'attaque
                        Color = randomColor // Couleur aléatoire
                    };

                    // Ajoute l'entrée du graphique à la liste
                    chartEntries.Add(entry);
                }

                // Initialise le graphique observable avec les entrées créées
                MyObservableChart = new BarChart
                {
                    Entries = chartEntries.OrderBy(entry => entry.Value).ToList(), // Trie les entrées par ordre croissant de valeur
                    ValueLabelOrientation = Orientation.Horizontal, // Orientation des valeurs
                    LabelTextSize = 30, // Taille du texte des étiquettes
                    LabelOrientation = Orientation.Horizontal // Orientation des étiquettes
                };
            }
        }
    }
}
