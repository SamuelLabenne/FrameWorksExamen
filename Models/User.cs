using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace FrameWorksExamen.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }  
        public string LastName { get; set; }
        [DefaultValue(false)]
        public bool deleted { get; set; }
    }
}
