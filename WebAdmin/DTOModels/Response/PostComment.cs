using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public class PostComment
    {

        public Guid? CommentId { get; set; }
        public string? Comment { get; set; }
        public string? CommentType { get; set; }
        public string? Status { get; set; }
        public Guid? ParentCommentId { get; set; }
        public Guid? MemberId { get; set; }
        public Guid? PostId { get; set; }
        public DateTime? CreateTime { get; set; }
        public ICollection<PostComment>? InverseParentComment { get; set; }
    }
}
