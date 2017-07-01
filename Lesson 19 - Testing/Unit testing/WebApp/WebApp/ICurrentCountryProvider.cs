using System;
using WebApp.Models;

namespace WebApp
{
    public interface ICurrentCountryProvider
    {
        Countries GetCurrentCountry();
    }
}
