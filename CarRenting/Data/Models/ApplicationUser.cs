using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using static CarRenting.Data.DataConstants.User;

namespace CarRenting.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(FullNameMaxLength)]
        public string FullName { get; set; }
    }
}
