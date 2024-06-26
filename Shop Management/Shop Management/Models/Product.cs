﻿using System.ComponentModel.DataAnnotations;

namespace Shop_Management.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Name field is required")]
        public string Name { get; set; }
        public string? Description { get; set; }

        public decimal Price { get; set; }

    }
}
