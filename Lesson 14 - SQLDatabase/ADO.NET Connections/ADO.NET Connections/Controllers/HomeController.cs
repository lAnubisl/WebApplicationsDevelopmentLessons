using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADO.NET_Connections.Models;
using Dapper;

namespace ADO.NET_Connections.Controllers
{
    public class Cat
    {
        public string Name { get; set; }
        public string Owner { get; set; }
        public int Id { get; set; }
    }

    public class MyContext : DbContext
    {
        public MyContext(string connectionString) : base(connectionString)
        {
        }

        public DbSet<Cat> Cats { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    public class HomeController : Controller
    {
        string connectionString = "Data Source=PANFILENOKA\\SQLEXPRESS;Initial Catalog=Webinar2017;User ID=default;Password=default";

        // GET: Home
        public ActionResult Index(string owner)
        {
            if (owner == null) owner = string.Empty;

            List<string> model = new List<string>();
            using (var context = new MyContext(connectionString))
            {
                model = context.Cats.Where(c => c.Owner == owner).Select(c => c.Name).ToList();
            }

            return View(model);
        }

        public ActionResult Edit(string name)
        {
            if (name == null) name = string.Empty;


            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("select * from Cat where name = @name", connection);
            command.Parameters.AddWithValue("@name", name);
            SqlDataReader dataReader = command.ExecuteReader();
            CatEditModel model = new CatEditModel();
            if (dataReader.Read())
            {
                model.Id = (int)dataReader["Id"];
                model.Name = (string) dataReader["Name"];
                model.Owner = (string) dataReader["Owner"];
            }

            dataReader.Close();
            connection.Close();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CatEditModel model)
        {
            using (var context = new MyContext(connectionString))
            {
                if (model.Id > 0)
                {
                    var cat = context.Cats.SingleOrDefault(c => c.Id == model.Id);
                    cat.Name = model.Name;
                    cat.Owner = model.Owner;
                    context.SaveChanges();
                }
                else
                {
                    var cat = new Cat();
                    cat.Name = model.Name;
                    cat.Owner = model.Owner;
                    context.Cats.Add(cat);
                    context.SaveChanges();
                }
            }

            return View();
        }
    }
}