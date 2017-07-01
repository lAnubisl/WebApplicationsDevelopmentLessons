using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp
{
    public class ProductPresentationService : IProductPresentationService
    {
        private readonly ITaxesProvider taxesProvider;
        private readonly ICurrentCountryProvider currentCountryProvider;

        public ProductPresentationService(ITaxesProvider taxesProvider, ICurrentCountryProvider currentCountryProvider)
        {
            this.taxesProvider = taxesProvider;
            this.currentCountryProvider = currentCountryProvider;
        }

        public ICollection<Product> GetTop(int top)
        {
            var result = new List<Product>();

            var price = 100;
            var country = currentCountryProvider.GetCurrentCountry();

            while (top > 0)
            {
                result.Add(new Product("Name", CalculateWithTax(price, country)));
                top--;
            }

            return result;
        }

        private decimal CalculateWithTax(decimal price, Countries country)
        {
            return price + price*(taxesProvider.GetCurrentTaxValue(country)/100);
        }
    }
}