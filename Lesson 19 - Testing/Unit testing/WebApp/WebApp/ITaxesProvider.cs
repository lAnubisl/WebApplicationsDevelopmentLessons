using WebApp.Models;

namespace WebApp
{
    public interface ITaxesProvider
    {
        decimal GetCurrentTaxValue(Countries country);
    }
}