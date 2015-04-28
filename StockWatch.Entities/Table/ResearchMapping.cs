using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockWatch.Entities.Table
{
    public class ResearchMapping: EntityTypeConfiguration<Research>
    {
        public ResearchMapping()
        {
            this.HasKey(x => x.Name);
            this.ToTable("Research");
        }
    }
}
