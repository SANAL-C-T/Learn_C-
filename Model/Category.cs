using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MenuApp.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; } // Unique identifier for the category

        [Column(TypeName = "nvarchar(100)")]
        public string CategoryName { get; set; } = string.Empty; // Category name
    }
}
