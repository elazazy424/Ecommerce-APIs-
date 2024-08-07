﻿using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Api.Dtos
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(0.1,double.MaxValue,ErrorMessage ="Price Must Be Greater Than Zero")]
        public decimal Price { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Quantity Must Be Greater Than One")]
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string? PictureUrl { get; set; }
        [Required]
        public string? Brand { get; set; }
        [Required]
        public string? Type { get; set; }
    }
}