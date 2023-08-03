using Binte.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Binte.WebApp.ViewModels.Account
{
    public class UserListViewModel
    {
        public int Id { get; set; }
        public string NameSurname { get; set; }
        public string Username { get; set; }
        public DateTime Birthdate { get; set; }
        public string EmailAddress { get; set; }
        public string Role { get; set; }
    }
}
