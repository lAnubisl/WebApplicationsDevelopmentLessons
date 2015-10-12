using System.Collections.ObjectModel;

namespace SimpleWebApp.Models
{
    public class CategoryProvider
    {
        public Collection<Category> Categorues()
        {
            var result = new Collection<Category>();
            var c1 = new Category();
            c1.Name = "Pants";
            c1.Products = new Collection<Product>();

            var c1p1 = new Product();
            c1p1.Name = "Yellow pants";
            c1p1.Price = 12;

            var c1p2 = new Product();
            c1p2.Name = "Blue pants";
            c1p2.Price = 13;

            c1.Products.Add(c1p1);
            c1.Products.Add(c1p2);

            var c2 = new Category();
            c2.Name = "T-shirts";
            c2.Products = new Collection<Product>();

            var c2p1 = new Product();
            c2p1.Name = "White t-shirt";
            c2p1.Price = 12;

            var c2p2 = new Product();
            c2p2.Name = "Black t-shirt";
            c2p2.Price = 13;

            c2.Products.Add(c2p1);
            c2.Products.Add(c2p2);

            result.Add(c1);
            result.Add(c2);

            return result;
        }
    }
}