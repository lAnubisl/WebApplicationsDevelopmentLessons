using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using WebApp.Models;

namespace WebApp.Tests
{
    [TestFixture]
    public class PresentationServiceTests
    {
        [Test]
        public void GetTop_should_return_prices_with_taxes()
        {
            // AAA
            // Arrange
            var taxesProvider = new Mock<ITaxesProvider>();
            var currentCountryProvider = new Mock<ICurrentCountryProvider>();

            currentCountryProvider.Setup(p => p.GetCurrentCountry()).Returns(Countries.US);
            taxesProvider.Setup(p => p.GetCurrentTaxValue(Countries.US)).Returns(13m);

            IProductPresentationService service = new ProductPresentationService(
                taxesProvider.Object, currentCountryProvider.Object);

            // Act
            var products = service.GetTop(1);

            // Assert
            Assert.AreEqual(1, products.Count);
            Assert.AreEqual(113, products.First().Price);
        }
    }
}
