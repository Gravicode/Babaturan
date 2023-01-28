using GemBox.Document;
using Microsoft.CodeAnalysis;
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
        Public, Friend, Private
    }
    public class ContactDetails
    {
        public Contacts? Contacts { get; set; }
        public List<Address>? Addresses { get; set; }
    }
    public class Contacts
    {
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
    public class Address
    {
        public string? AddressType { get; set; }
        public string? DNO { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
    }
    public class Employee
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        
        public ContactDetails? ContactDetails { get; set; }
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
        [ForeignKey("Post")]
        public long PostId { set; get; }
        public string RepostByUserName { set; get; }
        [ForeignKey("UserProfile")]
        public long RepostByUserId { set; get; }
        public UserProfile RepostByUser { set; get; }
    }


    [Table("postlike")]
    public class PostLike
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public Post Post { set; get; }
        [ForeignKey("Post")]
        public long PostId { set; get; }
        [ForeignKey("UserProfile")]
        public long LikedByUserId { set; get; }
        public string LikedByUserName { set; get; }
        public UserProfile LikedByUser { set; get; }
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
        [ForeignKey("Post")]
        public long PostId { set; get; }
        [ForeignKey("UserProfile")]
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

    [Table("post")]
    public class Post
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        [ForeignKey("UserProfile")]
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

        public ICollection<Repost> Reposts { get; set; }
        public UserProfile User { get; set; }

        public ICollection<PostLike> PostLikes { get; set; }
        public ICollection<PostComment> PostComments { get; set; }

        public ICollection<CommentLike> CommentLikes { get; set; }
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
        public ICollection<Repost> Reposts { get; set; }
        public ICollection<PostLike> PostLikes { get; set; }
        public ICollection<PostComment> PostComments { get; set; }
        public ICollection<Post> Posts { get; set; }
        [InverseProperty(nameof(WorkExperience.User))]
        public ICollection<WorkExperience> WorkExperiences { get; set; }

        [InverseProperty(nameof(Notification.User))]
        public ICollection<Notification> Notifications { get; set; }
        [InverseProperty(nameof(MessageHeader.User))]
        public ICollection<MessageHeader> UserMessages { get; set; }

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
