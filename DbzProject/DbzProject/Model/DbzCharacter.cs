using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DbzProject.Model
{
    public class DbzCharacter
    {
        public string? IdCharacter { get; set;}
        public string? Name { get; set; }
        public string? Race { get; set; }
        public string? Picture { get; set; } 
        public string? Attack { get; set;}
        public string? UserId { get; set;}

        [JsonIgnore] // Ignore la sérialisation de cette propriété
        public User? UserCreator { get; set; }

        

        public DbzCharacter(string IdCharacter,string Name,string Race,string Picture,string Attack,string UserId)
        {
            this.IdCharacter = IdCharacter;
            this.Name = Name;
            this.Race = Race;
            this.Picture = Picture;
            this.Attack = Attack;
            this.UserId = UserId;
        }

    }
}
