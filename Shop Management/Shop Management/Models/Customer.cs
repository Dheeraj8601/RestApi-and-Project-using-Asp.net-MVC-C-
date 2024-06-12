using System.ComponentModel.DataAnnotations;

namespace Shop_Management.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="First Name is necessary")]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
