using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MenuApp.Model
{
    public class Admin
    {

        [Key]
        public int Id { get; set; } // Unique identifier for the admin

        [Column(TypeName = "nvarchar(100)")]
        public string UserName { get; set; } = string.Empty; // Admin's username

        [Column(TypeName = "nvarchar(200)")]
        public string PasswordHash { get; set; } = string.Empty; // admin password
    }
}
