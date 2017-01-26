using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class PageModel
    {
        private readonly string name;
        private readonly int age;

        internal PageModel()
        {
        }

        internal PageModel(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public string Name
        {
            get { return this.name; }
        }

        public int GetAge()
        {
            return this.age;
        }


        public string Name2 { get; set; }

        public int Age2 { get; set; }
    }
}