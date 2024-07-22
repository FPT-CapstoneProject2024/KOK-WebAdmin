using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public partial class VoiceAudio
    {
        public Guid? VoiceId { get; set; }
        public string? VoiceUrl { get; set; }
        public double? DurationSecond { get; set; }
        public DateTime? UploadTime { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? Pitch { get; set; }
        public Guid? RecordingId { get; set; }
        public Guid? MemberId { get; set; }

    }
}
