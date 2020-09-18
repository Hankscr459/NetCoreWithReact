using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class AddUser : IdentityUser
    {
        public string DisPlayName { get; set; }
    }
}