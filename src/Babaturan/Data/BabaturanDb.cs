using Microsoft.EntityFrameworkCore;
using Babaturan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Babaturan.Helpers;
using Microsoft.Extensions.Options;

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
        //public DbSet<Author> Authors { get; set; }
        public DbSet<CommentLike> CommentLikes { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<CustomGroup> CustomGroups { get; set; }
        public DbSet<CustomPage> CustomPages { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<MyActivity> MyActivitys { get; set; }
        public DbSet<PictureAlbum> PictureAlbums { get; set; }
        public DbSet<PicturePost> PicturePosts { get; set; }
        public DbSet<PostView> PostViews { get; set; }
        public DbSet<ReportPost> ReportPosts { get; set; }
        public DbSet<BlockedPost> BlockedPosts { get; set; }
        public DbSet<HidePost> HidePosts { get; set; }
        public DbSet<SavedPost> SavedPosts { get; set; }
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
        public DbSet<PostDislike> PostDislikes { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<MessageHeader> MessageHeaders { get; set; }
        public DbSet<MessageDetail> MessageDetails { get; set; }
        public DbSet<MessageAttachment> MessageAttachments { get; set; }
       
        protected override void OnModelCreating(ModelBuilder builder)
        {
           
            //builder.Entity<Author>()
            //.Property(b => b.Contact)
            //.HasColumnType("LONGTEXT")
            //.HasJsonConversion<ContactDetails>();

            builder.Entity<Post>()
            .Property(b => b.Location)
            .HasColumnType("LONGTEXT")
            .HasJsonConversion<PostLocation?>();

            builder.Entity<Post>()
           .Property(b => b.Video)
           .HasColumnType("LONGTEXT")
           .HasJsonConversion<PostVideo?>();

            builder.Entity<Post>()
           .Property(b => b.Event)
           .HasColumnType("LONGTEXT")
           .HasJsonConversion<PostEvent?>();

            builder.Entity<PostStory>()
          .Property(b => b.ListMedia)
          .HasColumnType("LONGTEXT")
          .HasJsonConversion<List<StoryMedia>?>();
            /*
            builder.Entity<Post>().OwnsOne(x => x.Location, y => { y.ToJson(); });
            builder.Entity<Post>().OwnsOne(x => x.Video, y => { y.ToJson(); });
            
            builder.Entity<Post>().OwnsOne(
              x => x.Event, ownedNavigationBuilder => {
                  ownedNavigationBuilder.ToJson();
                  ownedNavigationBuilder.OwnsOne(_ => _.EventLocation);
                  ownedNavigationBuilder.OwnsMany(_ => _.Guests);
                  ownedNavigationBuilder.OwnsMany(_ => _.Attachments);
                  ownedNavigationBuilder.OwnsMany(_ => _.Visitors);
                  ownedNavigationBuilder.OwnsMany(_ => _.Registered);
                  ownedNavigationBuilder.OwnsMany(_ => _.Attendance);
                  ownedNavigationBuilder.OwnsMany(_ => _.FAQs);
                  ownedNavigationBuilder.OwnsMany(_ => _.Hosts);
                  ownedNavigationBuilder.OwnsMany(_ => _.Schedules);
              }
             );
            
            
            
            builder.Entity<PostStory>().OwnsMany(
               x => x.ListMedia, ownedNavigationBuilder => {
                   ownedNavigationBuilder.ToJson();
                   ownedNavigationBuilder.OwnsMany(_ => _.Comments);
               }
              );
            */
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
                optionsBuilder.UseMySql(AppConstants.SQLConn,ServerVersion.AutoDetect(AppConstants.SQLConn),options=>options.UseMicrosoftJson());
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
