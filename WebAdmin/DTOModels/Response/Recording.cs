using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public partial class Recording
    {
        public Recording()
        {
            Posts = new HashSet<Post>();
            VoiceAudios = new HashSet<VoiceAudio>();
        }

        public Guid RecordingId { get; set; }
        public string RecordingName { get; set; } = null!;
        public int RecordingType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int Score { get; set; }
        public Guid SongId { get; set; }
        public Guid HostId { get; set; }
        public Guid OwnerId { get; set; }
        public Guid KaraokeRoomId { get; set; }


        public  ICollection<Post> Posts { get; set; }
        public  ICollection<VoiceAudio> VoiceAudios { get; set; }
    }
}
