using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FormValidation.Repository
{
    public static class CountriesRepository
    {
        public static readonly ICollection<string> Countries =
            new Collection<string> {"Russia", "Belarus", "Ukraine"};
    }
}