using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp
{
    public interface IProductPresentationService
    {
        ICollection<Product> GetTop(int top);
    }
}