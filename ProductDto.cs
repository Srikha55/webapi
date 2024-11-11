using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class ProductDto
    {
       
        [Required]
        public String Name { get; set; } = " ";
        [Required]
        public String Brand { get; set; } = " ";
        [Required]
        public String Category { get; set; } = " ";
        [Required]
        public decimal Price { get; set; } 
        [Required]
        public String Description { get; set; } = " ";


    }
}
