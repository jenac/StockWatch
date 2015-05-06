using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockWatch.Entities.Table
{
    public class EodMapping : EntityTypeConfiguration<Eod>
    {
        public EodMapping()
        {
            this.HasKey(e => new { e.Symbol, e.Date });
            this.Property(e => e.Symbol).HasMaxLength(16);
        }
    }
}
