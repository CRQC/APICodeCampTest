using System.ComponentModel.DataAnnotations;

namespace TheCodeCamp.Models
{
    public class TalkModel
    {

        public int TalkId { get; set; }
        public string Title { get; set; }
        [StringLength(4096, MinimumLength =100)]
        public string Abstract { get; set; }
        public int Level { get; set; }
        [Required()]
        public SpeakerModel Speaker { get; set; }

    }
}