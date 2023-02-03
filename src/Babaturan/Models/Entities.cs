using GemBox.Document;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Reflection;

namespace Babaturan.Models
{
    #region helpers model  
    public class StorageObject
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public string FileUrl { get; set; }
        public string ContentType { get; set; }
        public DateTime? LastUpdate { get; set; }
        public DateTime? LastAccess { get; set; }
    }
    public class StorageSetting
    {
        public string EndpointUrl { get; set; } = "https://is3.cloudhost.id";
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string Region { get; set; } = "USWest1";
        public string Bucket { get; set; }
        public string BaseUrl { get; set; }
        public bool Ssl { get; set; } = true;
        public StorageSetting()
        {

        }
        public StorageSetting(string Endpoint, string Accesskey, string Secretkey, string Region, string Bucket)
        {
            this.EndpointUrl = Endpoint;
            this.AccessKey = Accesskey;
            this.SecretKey = Secretkey;
            this.Region = Region;
            this.Bucket = Bucket;
            GenerateBaseUrl();
        }
        public void GenerateBaseUrl()
        {
            this.BaseUrl = EndpointUrl + "/{bucket}/{key}";
        }
    }
    public class OutputCls
    {
        public OutputCls()
        {
            this.IsSucceed = false;
            this.Message = "";
        }
        public object Data { get; set; }
        public string Message { get; set; }
        public bool IsSucceed { get; set; }
    }
    public class PeopleByJob
    {
        public string Job { get; set; }
        public List<UserProfile> Users { get; set; }
    }
    public record PopularPeople(string Username, int TotalFollower, UserProfile User);
    #endregion
    public enum AccessTypes
    {
        Public, Friend, Private,Custom
    }
  
    [Table("post_story")]
    public class PostStory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        [ForeignKey(nameof(User)), Column(Order = 0)]
        public long UserId { get; set; }
        public UserProfile User { set; get; }
        public string UserName { set; get; }
        public DateTime PostDate { set; get; }
        public AccessTypes AccessType { get; set; } = AccessTypes.Public;
        public List<StoryMedia>? ListMedia { get; set; }
    }
    public enum StoryTypes
    {
        Image,Video
    }
    public class MediaComment
    {
        public DateTime CommentDate { get; set; }
        public string Comment { get; set; }
        public string UserName { set; get; }
    }
    public class StoryMedia
    {
        public int Order { get; set; } = 0;
        public string MediaUrl { get; set; }
        public StoryTypes StoryType { get; set; } = StoryTypes.Image;
        public string? Desc { get; set; }

        public List<MediaComment>? Comments { get; set; }
    }
    [Table("friend_request")]
    public class FriendRequest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        [ForeignKey(nameof(User)), Column(Order = 0)]
        public long UserId { get; set; }
        public UserProfile User { set; get; }
        public string UserName { set; get; }
        public DateTime RequestDate { set; get; }

        public bool? IsApproved { get; set; }
    }
    [Table("page_view")]
    public class PageView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        public string PageName { set; get; }
        public string PageUrl { set; get; }
        [ForeignKey(nameof(User)), Column(Order = 0)]
        public long UserId { get; set; }
        public UserProfile User { set; get; }
        public string UserName { set; get; }
        public string Agent { set; get; }
        public DateTime HitDate { set; get; }
    }
    [Table("message_header")]
    public class MessageHeader
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        public string Uid { set; get; }
        [ForeignKey(nameof(FromUser)), Column(Order = 0)]
        public long FromUserId { set; get; }
        public UserProfile FromUser { set; get; }
        [ForeignKey(nameof(User)), Column(Order = 1)]
        public long UserId { set; get; }
        public UserProfile User { set; get; }
        public string? Title { set; get; }
        public string? Desc { set; get; }
        public DateTime CreatedDate { set; get; }
        //public MessageTypes MessageType { set; get; }
        //public string MemberIds { set; get; }
        //public int MemberCount { set; get; } = 1;
        public string? LastMessage { set; get; }
        public DateTime LastUpdate { set; get; }
        public bool IsArchived { set; get; } = false;
        public bool IsRead { set; get; } = false;
        //public string? WallpaperUrl { set; get; }
        public bool IsMuted { set; get; } = false;
        public bool IsBlocked { set; get; } = false;
    }

    [Table("message_detail")]
    public class MessageDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        public string Uid { set; get; }
        [ForeignKey(nameof(MessageHeader)), Column(Order = 0)]
        public long MessageHeaderId { set; get; }
        public MessageHeader MessageHeader { get; set; }
        public DateTime CreatedDate { set; get; }
        [ForeignKey(nameof(FromUser)), Column(Order = 1)]
        public long FromUserId { set; get; }
        public UserProfile FromUser { set; get; }
        public string Message { set; get; }

        public bool HasAttachment { get; set; } = false;

        [InverseProperty(nameof(MessageAttachment.MessageDetail))]
        public ICollection<MessageAttachment> MessageAttachments { get; set; }
    }
    [Table("message_attachment")]
    public class MessageAttachment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        [ForeignKey(nameof(MessageDetail)), Column(Order = 0)]
        public long MessageDetailId { set; get; }
        public MessageDetail MessageDetail { set; get; }
        public string MessageHeaderUid { set; get; }
        public string MessageDetailUid { set; get; }
        public string Title { set; get; }
        public string? Url { set; get; }
        public string? Desc { set; get; }
        public string? Longitude { set; get; }
        public string? Latitude { set; get; }

        public AttachmentTypes AttachmentType { set; get; }
        public DateTime CreatedDate { set; get; }
    }
    public enum AttachmentTypes { Doc, Video, Audio, Link, Location };


    [Table("notification")]
    public class Notification
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        public DateTime CreatedDate { set; get; }
        [ForeignKey(nameof(User)), Column(Order = 0)]
        public long UserId { set; get; }
        public UserProfile User { set; get; }
        public string Action { set; get; }
        public string LinkUrl { set; get; }
        public string LinkDesc { set; get; }
        public string Message { set; get; }
        public bool IsRead { set; get; } = false;
    }

    [Table("trending")]
    public class Trending
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public string Hashtag { set; get; }
        public DateTime CreatedDate { set; get; }
        public string? Location { set; get; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
    }

    [Table("follow")]
    public class Follow
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        //user
        [ForeignKey(nameof(User)), Column(Order = 0)]
        public long UserId { get; set; }
        public UserProfile User { set; get; }
        public string UserName { set; get; }
        //follow
        public string FollowUserName { set; get; }

        [ForeignKey(nameof(FollowUser)), Column(Order = 1)]
        public long FollowUserId { get; set; }
        public UserProfile FollowUser { set; get; }
        public DateTime FollowDate { set; get; }
    }

    [Table("repost")]
    public class Repost
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public Post Post { set; get; }
        [ForeignKey(nameof(Post))]
        public long PostId { set; get; }
        public string RepostByUserName { set; get; }
        [ForeignKey(nameof(RepostByUser))]
        public long RepostByUserId { set; get; }
        public UserProfile RepostByUser { set; get; }
    }
    [Table("saved_post")]
    public class SavedPost
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public Post Post { set; get; }
        [ForeignKey(nameof(Post))]
        public long PostId { set; get; }
        [ForeignKey(nameof(User))]
        public long UserId { set; get; }
        public string UserName { set; get; }
        public UserProfile User { set; get; }
        public DateTime CreatedDate { set; get; }
    } 
    [Table("hide_post")]
    public class HidePost
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public Post Post { set; get; }
        [ForeignKey("Post")]
        public long PostId { set; get; }
        [ForeignKey("UserProfile")]
        public long UserId { set; get; }
        public string UserName { set; get; }
        public UserProfile User { set; get; }
        public DateTime CreatedDate { set; get; }
    } 
    
    [Table("blocked_post")]
    public class BlockedPost
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
     
        [ForeignKey(nameof(Post))]
        public long PostId { set; get; }
        public Post Post { set; get; }
        [ForeignKey(nameof(User))]
        public long UserId { set; get; }
        public string UserName { set; get; }
        public UserProfile User { set; get; }
        public DateTime CreatedDate { set; get; }
    }
    public enum ReportPostTypes { Racist, Hate, Porn, Illegal, IDontLike }
    [Table("report_post")]
    public class ReportPost
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public Post Post { set; get; }
        [ForeignKey("Post")]
        public long PostId { set; get; }
        [ForeignKey("UserProfile")]
        public long UserId { set; get; }
        public string UserName { set; get; }
        public UserProfile User { set; get; }
        public ReportPostTypes ReportType { get; set; }
        public string Complain { get; set; }
        public DateTime CreatedDate { set; get; }
    }
    [Table("user_group")]
    public class UserGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        [ForeignKey(nameof(User)), Column(Order = 0)]
        public long UserId { set; get; }
        public UserProfile User { set; get; }

        [ForeignKey(nameof(Group)), Column(Order = 1)]
        public long GroupId { set; get; }
        public CustomGroup Group { set; get; }
        public DateTime JoinDate { set; get; }
       

    }
    [Table("custom_group")]
    public class CustomGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        [ForeignKey(nameof(User)), Column(Order = 0)]
        public long UserId { set; get; }
        public UserProfile User { set; get; }
        public DateTime CreatedDate { set; get; }
        public string GroupName { set; get; }
        public string? ImageUrl { set; get; }
        public AccessTypes AccessType { set; get; }
      
        public string? Desc { set; get; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<UserGroup> UserGroups { get; set; }

    }
    [Table("custom_page")]
    public class CustomPage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
   
        [ForeignKey(nameof(User)), Column(Order = 0)]
        public long UserId { set; get; }
        public UserProfile User { set; get; }
        public DateTime CreatedDate { set; get; }
        public string PageName { set; get; }
        public string DisplayName { set; get; }
        public string? Email { set; get; }
        public string? Category { set; get; }
        public string? Website { set; get; }
        public string? PhoneNumber { set; get; }
        public string? About { set; get; }
        public string? FBUrl { set; get; }
        public string? TwitterUrl { set; get; }
        public string? InstagramUrl { set; get; }
        public string? LinkedInUrl { set; get; }
        


    }

    [Table("blog")]
    public class Blog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public string Uid { set; get; }
        [ForeignKey(nameof(User)), Column(Order = 0)]
        public long UserId { set; get; }
        public UserProfile User { set; get; }
        public DateTime CreatedDate { set; get; }
        public string Body { set; get; }
        public string Title { set; get; }
        public string? Tags { set; get; }
        public string? Category { set; get; }
        public string? ImageUrl { set; get; }
        public bool Featured { get; set; } = false;

        public ICollection<BlogComment> BlogComments { get; set; }

    }

    [Table("blog_comment")]
    public class BlogComment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        [ForeignKey(nameof(Blog)), Column(Order = 0)]
        public long BlogId { set; get; }
        public Blog Blog { set; get; }
        public string Name { set; get; }
        public string Email { set; get; }
        public string Comment { set; get; }
        public DateTime CreatedDate { set; get; }
    }
    [Table("postlike")]
    public class PostLike
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public Post Post { set; get; }
        [ForeignKey(nameof(Post))]
        public long PostId { set; get; }
        [ForeignKey(nameof(LikedByUser))]
        public long LikedByUserId { set; get; }
        public string LikedByUserName { set; get; }
        public UserProfile LikedByUser { set; get; }
        public DateTime CreatedDate { set; get; }
    } 
    
    [Table("postdislike")]
    public class PostDislike
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public Post Post { set; get; }
        [ForeignKey(nameof(Post))]
        public long PostId { set; get; }
        [ForeignKey("UserProfile")]
        public long DislikedByUserId { set; get; }
        public string DislikedByUserName { set; get; }
        public UserProfile DislikedByUser { set; get; }
        public DateTime CreatedDate { set; get; }
    }
    [Table("my_activity")]
    public class MyActivity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Activity { get; set; }
        [ForeignKey("UserProfile")]
        public long UserId { set; get; }
        public string UserName { set; get; }
        public UserProfile User { set; get; }
        public Post? Post { set; get; }
        [ForeignKey("Post")]
        public long? PostId { set; get; }
        public DateTime ActivityDate { set; get; }
    }

    [Table("picture_album")]
    public class PictureAlbum
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Desc { get; set; }
        public AccessTypes AccessType { get; set; } = AccessTypes.Public;
        [ForeignKey("UserProfile")]
        public long UserId { set; get; }
        public string UserName { set; get; }
        public UserProfile User { set; get; }

        public List<PicturePost>? PicturePosts { get; set; }
        public DateTime CreatedDate { set; get; }
    }
    
    [Table("picture_post")]
    public class PicturePost
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
       
        [ForeignKey("Album")]
        public long AlbumId { set; get; }
      
        public PictureAlbum Album { set; get; }

        [ForeignKey("Post")]
        public long PostId { set; get; }

        public Post Post { set; get; }
        public DateTime CreatedDate { set; get; }
    }

    [Table("postview")]
    public class PostView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public Post Post { set; get; }
        [ForeignKey("Post")]
        public long PostId { set; get; }
        [ForeignKey("UserProfile")]
        public long UserId { set; get; }
        public string UserName { set; get; }
        public UserProfile User { set; get; }
        public DateTime CreatedDate { set; get; }
    }

    [Table("postcomment")]
    public class PostComment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public string Comment { set; get; }
        public Post Post { set; get; }
        [ForeignKey(nameof(Post))]
        public long PostId { set; get; }
        [ForeignKey(nameof(User))]
        public long UserId { set; get; }
        public string Username { set; get; }
        public DateTime CreatedDate { set; get; }
        public UserProfile User { set; get; }
        public ICollection<CommentLike> CommentLikes { get; set; }

    }
    [Table("commentlike")]
    public class CommentLike
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public PostComment Comment { set; get; }
        [ForeignKey("Comment")]
        public long CommentId { set; get; }
        [ForeignKey("UserProfile")]
        public long LikedByUserId { set; get; }
        public string LikedByUserName { set; get; }
        public UserProfile LikedByUser { set; get; }
        public DateTime CreatedDate { set; get; }
    }

    [Table("contact")]
    public class Contact
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public string Email { set; get; }
        public string FullName { set; get; }
        public string Phone { set; get; }
        public string Message { set; get; }
        public DateTime CreatedDate { set; get; }
        public bool IsReplied { get; set; } = false;
        public string? ReplyBy { get; set; }
        public string? ReplyMessage { get; set; }
        public DateTime? ReplyDate { get; set; }
    }

    public enum PostTypes
    {
        ImagePost, VideoPost, EventPost, ActivityPost, PollPost, QuestionPost
    }
    public class PostLocation
    {
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class AttachmentItem
    {
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string FileType { get; set; }
    }
    public class EventSchedule
    {
        public DateTime ScheduleTime { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
    }

    public class EventHost
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string PicUrl { get; set; }
    }

    public class EventFAQ
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
    public enum EventTypes { Online, Offline }
    public class ItemString
    {
        public string ItemValue { get; set; }
    }
    public class PostEvent
    {
        public string Title { get; set; }
        public string? Desc { get; set; }
        public string? Category { get; set; }
        public EventTypes EventType { get; set; }
        public DateTime EventDate { get; set; }
        public TimeSpan EventTime { get; set; }
        public string Duration { get; set; }
        public PostLocation EventLocation { get; set; }
        public List<ItemString> Guests { get; set; }
        public List<AttachmentItem> Attachments { get; set; }
        public List<ItemString> Visitors { get; set; }
        public List<ItemString> Registered { get; set; }
        public List<ItemString> Attendance { get; set; }
        public List<EventFAQ> FAQs { get; set; }
        public List<EventHost> Hosts { get; set; }
        public List<EventSchedule> Schedules { get; set; }

    }
    public class PostVideo
    {
        public string? Title { get; set; }
        public string? Category { get; set; }
        public string? ThumbnailUrl { get; set; }
        public TimeSpan? Duration { get; set; }
    }
    [Table("post")]
    public class Post
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        [ForeignKey(nameof(User))]
        public long UserId { set; get; }
        public string UserName { set; get; }
        public DateTime CreatedDate { set; get; }
        public string Message { set; get; }
        public string? Mentions { set; get; }
        public string? LinkUrls { set; get; }
        public string? VideoUrls { set; get; }
        public string? DocUrls { set; get; }
        public string? ImageUrls { set; get; }
        public string? Hashtags { set; get; }

        public bool IsBlocked { get; set; } = false;
        public AccessTypes AccessType { get; set; }
        public PostTypes PostType { get; set; } = PostTypes.ActivityPost;
        public PostLocation? Location { get; set; }
        public PostEvent? Event { get; set; }
        public PostVideo? Video { get; set; }
        [InverseProperty(nameof(Repost.Post))]
        public ICollection<Repost> Reposts { get; set; }
        public UserProfile User { get; set; }
        [InverseProperty(nameof(PostLike.Post))]
        public ICollection<PostLike> PostLikes { get; set; }
        [InverseProperty(nameof(PostDislike.Post))]
        public ICollection<PostDislike> PostDislikes { get; set; }
        [InverseProperty(nameof(PostComment.Post))]
        public ICollection<PostComment> PostComments { get; set; }
        [InverseProperty(nameof(BlockedPost.Post))]
        public ICollection<BlockedPost> BlockedPosts { get; set; }
        public bool IsGroupPost { get; set; } = false;

        [ForeignKey(nameof(CustomGroup))]
        public long? GroupId { set; get; }
        public CustomGroup? Group { get; set; }

    }

    public enum LogCategory
    {
        Info, Error, Warning
    }
    [Table("logs")]
    public class Log
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LogDate { get; set; }
        public string Message { get; set; }
        public LogCategory Category { get; set; }
    }

    [Table("userprofile")]
    public class UserProfile
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsBlueBadge { get; set; } = true;
        public string? Job { get; set; } = "Manusia";
        public string? Website { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Alamat { get; set; }
        public string? KTP { get; set; }
        public string? PicUrl { get; set; }
        public bool Aktif { get; set; } = true;
        public string? Daerah { get; set; }
        public string? Desa { get; set; }
        public string? Kelompok { get; set; }
        public Char Gender { get; set; } = 'N';
        public Roles Role { set; get; } = Roles.User;
        public string? AboutMe { get; set; } = "Manusia biasa.";
        public DateTime CreatedDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }

        [InverseProperty(nameof(Follow.FollowUser))]
        public ICollection<Follow> Follows { get; set; }

        [InverseProperty(nameof(Follow.User))]
        public ICollection<Follow> FollowedBy { get; set; }
        [InverseProperty(nameof(Repost.RepostByUser))]
        public ICollection<Repost> Reposts { get; set; }
        [InverseProperty(nameof(PostLike.LikedByUser))]
        public ICollection<PostLike> PostLikes { get; set; }
        [InverseProperty(nameof(PostComment.User))]
        public ICollection<PostComment> PostComments { get; set; }
        [InverseProperty(nameof(Post.User))]
        public ICollection<Post> Posts { get; set; }
        [InverseProperty(nameof(WorkExperience.User))]
        public ICollection<WorkExperience> WorkExperiences { get; set; }

        [InverseProperty(nameof(Notification.User))]
        public ICollection<Notification> Notifications { get; set; }
        [InverseProperty(nameof(MessageHeader.User))]
        public ICollection<MessageHeader> UserMessages { get; set; } 

        [InverseProperty(nameof(SavedPost.User))]
        public ICollection<SavedPost> SavedPosts { get; set; }

        public bool EnableLikesAndComment { get; set; } = true;
        public bool EnableReplyToMyComment { get; set; } = true;
        public bool EnableSubscriptions { get; set; } = true;
        public bool EnableBirthday { get; set; } = true;
        public bool EnableEvent { get; set; } = true;
        public bool EnableEmailNotification { get; set; } = true;
        public bool EnablePushNotification { get; set; } = true;
        public bool EnableWeeklyAccountSummary { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public SocialStatus Status { get; set; }
        public string? WorkAt { get; set; }
        public string? Education { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Interest { get; set; }
    }

    [Table("work_experience")]
    public class WorkExperience
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        public DateTime? StartDate { set; get; }
        public DateTime? EndDate { set; get; }
        [ForeignKey(nameof(User)), Column(Order = 0)]
        public long UserId { set; get; }
        public UserProfile User { set; get; }
        public string Company { set; get; }
        public string Desc { set; get; }
    }
    public enum SocialStatus { Single, Married }
    public enum Roles { Admin, User, Pengurus }


}
