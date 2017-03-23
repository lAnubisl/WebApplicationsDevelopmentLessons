using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TempStorage.Models
{
    [Serializable]
    public class User
    {
        public string Name { get; set; }

        public Address address { get; set; }

    }

    [Serializable]
    public class Address
    {
        private string street { get; set; }
    }
}