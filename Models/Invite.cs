using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameWorksExamen.Models
{
    public class Invite
    {
        public int Id { get; set; }
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public Person? Person { get; set; }
        [ForeignKey("Event")]
        public int EventId { get; set; }
        public Event Event { get; set; }

        [DefaultValue(false)]
        public bool deleted { get; set; }
    }
}
