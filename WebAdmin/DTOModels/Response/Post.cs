﻿using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public partial class Post
    {
        public Guid? PostId { get; set; }
        public string? Caption { get; set; }
        public DateTime? UploadTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public Guid? MemberId { get; set; }
        public Guid? RecordingId { get; set; }
        public string? PostStatus { get; set; }
        public string? PostType { get; set; }
        public string? SongUrl { get; set; }
        public Guid? OriginPostId { get; set; }
        public string? ReportId { get; set; }

        public  ICollection<Post>? InverseOriginPost { get; set; }
        public  ICollection<PostRating>? PostRatings { get; set; }
        public  ICollection<PostShare>? PostShares { get; set; }
        public  ICollection<Report>? Reports { get; set; }
    }
}
