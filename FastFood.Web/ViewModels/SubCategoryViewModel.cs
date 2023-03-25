using FastFood.Models;

namespace FastFood.Web.ViewModels
{
    public class SubCategoryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int CategoryId { get; set; }
    }
}
