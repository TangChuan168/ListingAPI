using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dataAPI.Models
{
    public class DB:DbContext
    {
        public virtual DbSet<Listing> Listings { get; set; }
        public virtual DbSet<propertyDetails> Details { get; set; }
        public virtual DbSet<keyText> KeyTexts { get; set; }
        public virtual DbSet<Contacts> Contactz { get; set; }
        public virtual DbSet<PicUrl> PicUrls { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=47.74.86.28;port=3306;user=dbuser;password=MI4m481OuUJ1D9KijI921KFMRFHndvNi;database=TradeMeCrawler", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.1.48-mariadb"));
            }

        }

    }
}
