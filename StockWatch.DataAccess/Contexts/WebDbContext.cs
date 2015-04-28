using StockWatch.Entities.Table;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockWatch.DataAccess.Contexts
{
    public class WebDbContext : DbContext
    {
        public DbSet<Research> Researches { get; set; }

        public WebDbContext()
        {
            Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ResearchMapping());
            base.OnModelCreating(modelBuilder);

        }

        

        /*
         * new Dispose() ?
         * */
    }

}
