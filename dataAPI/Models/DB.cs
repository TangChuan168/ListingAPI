using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
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
                optionsBuilder.UseMySql("server=47.74.86.28;port=3306;user=dbuser;password=MI4m481OuUJ1D9KijI921KFMRFHndvNi;database=james", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.1.48-mariadb"));
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Listing mode
            modelBuilder.Entity<Listing>(entity =>
            {
                entity.HasKey(e => e.Guid);
                entity.Property(e => e.tagNo);//.IsRequired()
                entity.Property(e => e.Url);
                entity.Property(e => e.IsActive);
                entity.Property(e => e.ReType);
                entity.Property(e => e.options);
                entity.HasOne(x => x.propertyDetails).WithOne(c => c.Listing).HasForeignKey<propertyDetails>(p => p.ListingGuid);

            });

            // propertyDetails mode
            modelBuilder.Entity<propertyDetails>(entity =>
            {
                entity.HasKey(e => e.PPDguid);
                entity.Property(e => e.heading);
                entity.Property(e => e.AskPrice);
                entity.Property(e => e.Address);
                entity.Property(e => e.Descriptions);

                //entity.Property(e => e.PicUrls);
                //entity.Property(e => e.Contactz);
                entity.Property(e => e.UpdateTime);

                entity.Property(e => e.BedRoom);
                entity.Property(e => e.Study);
                entity.Property(e => e.Couch);
                entity.Property(e => e.Bath);
                entity.Property(e => e.Garage);
                entity.Property(e => e.Toilet);

                entity.Property(e => e.PropertyType);
                entity.Property(e => e.PropertyUse);
                entity.Property(e => e.SaleMethod);
                entity.Property(e => e.OpenHomeSessions);
                entity.Property(e => e.FloorArea);
                entity.Property(e => e.Reference);


            });

            // keyText mode
            modelBuilder.Entity<keyText>(entity =>
            {
                entity.HasKey(e => e.Textguid);
                entity.Property(e => e.kText).IsRequired();

                entity.HasOne(d => d.currentDetails)
                  .WithMany(p => p.Texts);
            });


            // Contacts mode
            modelBuilder.Entity<Contacts>(entity =>
            {
                entity.HasKey(e => e.Contaxtsguid);
                entity.Property(e => e.Name);

                entity.HasOne(d => d.currentDetails)
                  .WithMany(p => p.Contactz);

            });
            // PicUrl mode
            modelBuilder.Entity<PicUrl>(entity =>
            {
                entity.HasKey(e => e.Picguid);
                entity.Property(e => e.PictureUrl);
                entity.HasOne(d => d.propertyDetails)
                  .WithMany(p => p.PicUrls);

            });
        }

    }
}
