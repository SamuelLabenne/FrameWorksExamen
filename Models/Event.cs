using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FrameWorksExamen.Models
{
    public class Event
    {
        public int Id { get; set; }
        [Display(Name = "Naam")]
        public string Name { get; set; }
        [Display(Name = "Omschrijving")]
        public string Description { get; set; }
        public string Location { get; set; }
        [Display(Name = "Datum")]
        [DataType(DataType.Date)]
        public DateTime date { get; set; }
        [DataType(DataType.Text)]
        public string people { get; set; }
        [DefaultValue(false)]
        public bool deleted { get; set; }
    }
}
