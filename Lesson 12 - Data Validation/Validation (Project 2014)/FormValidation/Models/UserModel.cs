using System.Collections.Generic;
using System.Web;
using FormValidation.Repository;

namespace FormValidation.Models
{
    public class UserModel
    {
        public ICollection<string> Countries
        {
            get { return CountriesRepository.Countries; }
        }

        public HttpPostedFileWrapper Avatar { get; set; }

        public string Country { get; set; }

        public int Age { get; set; }

        public string Username { get; set; }
    }
}