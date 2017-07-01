using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public enum Countries
    {
        BY, RU, US, CA
    }

    //public class Taxes : Dictionary<Countries, decimal>
    //{
    //    private static Taxes instance;

    //    static Taxes()
    //    {
    //        instance = new Taxes();
    //    }

    //    private Taxes()
    //    {
    //        this.Add(Countries.BY, 12);
    //        this.Add(Countries.RU, 13);
    //        this.Add(Countries.CA, 15);
    //        this.Add(Countries.US, 20);
    //    }

    //    public static Taxes Instance => instance;
    //}

    public class Product
    {
        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; }

        public decimal Price { get; }
    }
}