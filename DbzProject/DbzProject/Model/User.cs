using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbzProject.Model
{
    public class User
    {
        public string IdUser { get; set; }
        public string Username { get; set; }
        public  string Password { get; set; }
        public ICollection<DbzCharacter> Characters { get; set; }

        public User(string IdUser,string username,string password)
        {
            this.IdUser = IdUser;
            this.Username = username;
            this.Password = password;
            this.Characters = new List<DbzCharacter>();
        }

    }

}
