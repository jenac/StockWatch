using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockWatch.Entities.Table
{
    public class CompanyMapping : EntityTypeConfiguration<Company>
    {
        public CompanyMapping()
        {
            this.HasKey(e => new { e.Symbol, e.Exchange });
            this.Property(e => e.Symbol).HasMaxLength(16);
            this.Property(e => e.Exchange).HasMaxLength(16);
            this.Property(e => e.Name).HasMaxLength(256);
            this.Property(e => e.Sector).HasMaxLength(256);
            this.Property(e => e.Industry).HasMaxLength(256);

        }
    }
}
