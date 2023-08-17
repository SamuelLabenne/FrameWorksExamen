using System.ComponentModel;

namespace FrameWorksExamen.Models
{
    public class Invite
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int EventId { get; set; }
        [DefaultValue(false)]
        public bool deleted { get; set; }
    }
}
