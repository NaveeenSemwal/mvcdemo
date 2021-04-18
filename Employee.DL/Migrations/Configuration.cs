namespace Employees.DL.Migrations
{
    using Employees.DL.Database;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Employees.DL.EMSDemoEntities>
    {
        /// <summary>
        /// https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-a-more-complex-data-model-for-an-asp-net-mvc-application
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Employees.DL.EMSDemoEntities context)
        {
            //  This method will be called after migrating to the latest version.

            var titles = new List<TitleMaster>
            {
                  new TitleMaster {  Title = "Miss." },
                  new TitleMaster {  Title = "Mr." },
                  new TitleMaster {  Title = "Mrs." }
            };

            //foreach (var title in titles)
            //{
            //    context.TitleMasters.AddOrUpdate(x => x.Title, title);
            //}

            // Above is same
            titles.ForEach(title => context.TitleMasters.AddOrUpdate(x => x.Title, title));

            context.SaveChanges();
            System.Console.WriteLine("Seed success");
        }

       
    }
}
