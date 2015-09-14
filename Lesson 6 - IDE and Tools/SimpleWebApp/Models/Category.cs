using System.Collections.ObjectModel;

namespace SimpleWebApp.Models
{
    public class Category
    {
        public string Name;
        public Collection<Product> Products;
        public Collection<Category> SubCategories;
    }
}