using Microsoft.Identity.Client;

namespace WebAppProject.ViewModels
{
    public class UserViewModel
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string DateBirth { get; set; }
    }
}
