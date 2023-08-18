using System.ComponentModel.DataAnnotations.Schema;

namespace FrameWorksExamen.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public bool deleted { get; set; }
        public List<Event>? Events { get; set; }
        [NotMapped]
        public List<int> EventsId { get; set; }
    }
}
