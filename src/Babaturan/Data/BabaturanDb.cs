using Microsoft.EntityFrameworkCore;
using Babaturan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace Babaturan.Data
{
    public class BabaturanDB : DbContext
    {

        public BabaturanDB()
        {
        }

        public BabaturanDB(DbContextOptions<BabaturanDB> options)
            : base(options)
        {
        }
        public DbSet<PostStory> PostStorys { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }
        public DbSet<PageView> PageViews { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Repost> Reposts { get; set; }
        public DbSet<Trending> Trendings { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<MessageHeader> MessageHeaders { get; set; }
        public DbSet<MessageDetail> MessageDetails { get; set; }
        public DbSet<MessageAttachment> MessageAttachments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            /*
            builder.Entity<DataEventRecord>().HasKey(m => m.DataEventRecordId);
            builder.Entity<SourceInfo>().HasKey(m => m.SourceInfoId);

            // shadow properties
            builder.Entity<DataEventRecord>().Property<DateTime>("UpdatedTimestamp");
            builder.Entity<SourceInfo>().Property<DateTime>("UpdatedTimestamp");
            */
          
            builder.Entity<PostStory>().OwnsMany(
             x => x.ListMedia, ownedNavigationBuilder => {
                 ownedNavigationBuilder.ToJson();
                 ownedNavigationBuilder.OwnsMany(_ => _.Comments);
             }
            );
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            /*
            ChangeTracker.DetectChanges();

            updateUpdatedProperty<SourceInfo>();
            updateUpdatedProperty<DataEventRecord>();
            */
            return base.SaveChanges();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(AppConstants.SQLConn,ServerVersion.AutoDetect(AppConstants.SQLConn));
            }
        }
        private void updateUpdatedProperty<T>() where T : class
        {
            /*
            var modifiedSourceInfo =
                ChangeTracker.Entries<T>()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in modifiedSourceInfo)
            {
                entry.Property("UpdatedTimestamp").CurrentValue = DateTime.UtcNow;
            }
            */
        }

    }
}
