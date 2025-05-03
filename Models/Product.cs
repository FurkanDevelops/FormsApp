using System.ComponentModel.DataAnnotations;

namespace FormsApp.Models
{
    public class Product{
        [Display(Name = "Product Id")]
        public int ProductId { get; set; }


        [Display(Name = "Product Name")]
        [Required(ErrorMessage ="Prodıct Name is required")]
        public string? Name { get; set; }


        [Display(Name ="Price")]
        [Required(ErrorMessage = "Price is required")]
        [Range(0,1000000,ErrorMessage ="Price must be between 0 and 1000000")]
        public decimal? Price { get; set; }


        [Display(Name = "Image")]
        public string? ImageUrl { get; set; }
        [Display(Name="Is Active")]
        public bool IsActive { get; set; }

        [Display(Name ="Category")]
        [Required(ErrorMessage = "Category is required")]
        public int? CategoryId { get; set; }
    }
}