//using System;
//using APBD_PRO.Server.Models;
////using APBD_PRO.Shared;
//using Microsoft.EntityFrameworkCore;

//namespace APBD_PRO.Server.Data
//{
//    public class DatabaseContext : DbContext
//    {
//        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
//        {
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<FullTickerDb>(e =>
//            {
//                e.HasKey(e => e.ticker);

//                e.Property(e => e.name).HasMaxLength(255).IsRequired();
//                e.Property(e => e.locale).HasMaxLength(6).IsRequired();
//                e.Property(e => e.primary_exchange).HasMaxLength(6).IsRequired();
//                e.Property(e => e.description).HasMaxLength(255).IsRequired();
//                e.Property(e => e.homepage_url).HasMaxLength(255).IsRequired();
//                e.Property(e => e.total_employees).IsRequired();
//                e.Property(e => e.phone_number).HasMaxLength(255).IsRequired();
//                e.Property(e => e.list_date).HasMaxLength(255).IsRequired();
//                e.Property(e => e.icon_url).HasMaxLength(255).IsRequired();
//                e.Property(e => e.logo_url).HasMaxLength(255).IsRequired();
//                e.Property(e => e.address1).HasMaxLength(255).IsRequired();
//                e.Property(e => e.city).HasMaxLength(255).IsRequired();
//                e.Property(e => e.postal_code).HasMaxLength(255).IsRequired();
//                e.Property(e => e.state).HasMaxLength(255).IsRequired();

//                e.ToTable("FullTicker");
//            });

//            modelBuilder.Entity<WatchlistDb>(e =>
//            {
//                e.HasKey(e => new { e.user_id, e.ticker });

//                e.HasOne(e => e.fullTicker)
//                .WithMany(e => e.Watchlists)
//                .HasForeignKey(e => e.ticker)
//                .OnDelete(DeleteBehavior.Cascade);

//                e.HasOne(e => e.applicationUser)
//                .WithMany(e => e.watchlists)
//                .HasForeignKey(e => e.user_id)
//                .OnDelete(DeleteBehavior.Cascade);

//                e.ToTable("Watchlist");
//            });

//            modelBuilder.Entity<ChartDataDb>(e =>
//            {
//                e.HasKey(e => e.chart_id);

//                e.HasOne(e => e.fullTicker)
//                .WithMany(e => e.chartDatas)
//                .HasForeignKey(e => e.ticker)
//                .OnDelete(DeleteBehavior.Cascade);

//                e.Property(e => e.date).IsRequired();
//                e.Property(e => e.open).HasMaxLength(10).HasPrecision(2).IsRequired();
//                e.Property(e => e.low).HasMaxLength(10).HasPrecision(2).IsRequired();
//                e.Property(e => e.close).HasMaxLength(10).HasPrecision(2).IsRequired();
//                e.Property(e => e.high).HasMaxLength(10).HasPrecision(2).IsRequired();
//                e.Property(e => e.volume).HasMaxLength(10).HasPrecision(2).IsRequired();

//                e.ToTable("ChartData");
//            });

//            modelBuilder.Entity<TickerNewsDb>(e =>
//            {
//                e.HasKey(e => e.news_id);

//                e.Property(e => e.title).HasMaxLength(255).IsRequired();
//                e.Property(e => e.author).HasMaxLength(255).IsRequired();
//                e.Property(e => e.article_url).HasMaxLength(255).IsRequired();
//                e.Property(e => e.image_url).HasMaxLength(255).IsRequired();
//                e.Property(e => e.description).HasMaxLength(255).IsRequired();

//                e.ToTable("TickerNews");
//            });

//            modelBuilder.Entity<TickerInNewsDb>(e =>
//            {
//                e.HasKey(e => new { e.news_Id, e.ticker });

//                e.HasOne(e => e.tickerNews)
//                .WithMany(e => e.tickerInNews)
//                .HasForeignKey(e => e.news_Id)
//                .OnDelete(DeleteBehavior.Cascade);

//                e.HasOne(e => e.fullTicker)
//                .WithMany(e => e.tickerInNews)
//                .HasForeignKey(e => e.ticker)
//                .OnDelete(DeleteBehavior.Cascade);

//                e.ToTable("TickerInNews");
//            });


//        }
//    }
//}

