using FastFood.Models;

namespace FastFood.Web.ViewModels
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IFormFile ImageUrl { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
    }
}
