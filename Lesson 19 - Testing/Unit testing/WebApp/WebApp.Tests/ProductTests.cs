using System;
using System.Collections;
using NUnit.Framework;
using WebApp.Models;

namespace WebApp.Tests
{
    [TestFixture]
    public class ProductTests
    {
        //[Test, TestCaseSource(typeof(ProductTestsSource), "Source")]
        //public decimal Price_should_be_calculated_with_taxes(decimal price, Countries country)
        //{
        //    var product = new Product("Name", price, country);
        //    return product.Price;
        //}
    }

    public class ProductTestsSource
    {
        public static IEnumerable Source
        {
            get
            {
                yield return new TestCaseData(100m, Countries.BY).Returns(112m);
                yield return new TestCaseData(100m, Countries.RU).Returns(113m);
                yield return new TestCaseData(100m, Countries.CA).Returns(115m);
                yield return new TestCaseData(100m, Countries.US).Returns(120m);
            }
        }
    }
}
