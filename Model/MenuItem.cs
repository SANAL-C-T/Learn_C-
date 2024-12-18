using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MenuApp.Model
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; } // Unique identifier for the menu item

        [Column(TypeName = "nvarchar(100)")]
        public string ItemName { get; set; } = string.Empty; // Name of the menu item

        [Column(TypeName = "nvarchar(200)")]
        public string ItemDescription { get; set; } = string.Empty; // Description of the menu item

        [ForeignKey("CategoryId")] // Foreign key attribute to reference the Category model

        public int CategoryId { get; set; } // ID of the associated category
        public required Category Category { get; set; } // Navigation property for related Category, used by Entity Framework


        [Column(TypeName = "decimal(18, 2)")] // Using decimal for monetary values
        public decimal ItemPrice { get; set; } = 0.0m; // Price of the menu item
    }
}
