using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public partial class Post
    {
        public Post()
        {
            InverseOriginPost = new HashSet<Post>();
            PostRatings = new HashSet<PostRating>();
            PostShares = new HashSet<PostShare>();
            Reports = new HashSet<Report>();
        }

        public Guid PostId { get; set; }
        public string? Caption { get; set; }
        public DateTime UploadTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public Guid MemberId { get; set; }
        public Guid RecordingId { get; set; }
        public int Status { get; set; }
        public int PostType { get; set; }
        public Guid? OriginPostId { get; set; }

        public virtual Account Member { get; set; } = null!;
        public virtual Post? OriginPost { get; set; }
        public virtual Recording Recording { get; set; } = null!;
        public virtual PostComment? PostComment { get; set; }
        public virtual ICollection<Post> InverseOriginPost { get; set; }
        public virtual ICollection<PostRating> PostRatings { get; set; }
        public virtual ICollection<PostShare> PostShares { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
