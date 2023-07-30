using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models;
//one to many relationship (one user can be assign to one role, one role can have many users)
public class User
{
    public int UserId { get; set; }
    [MaxLength(100)]
    [Required(ErrorMessage ="Email Id is a required field!")]
    public string Email { get; set; } = string.Empty;
    [MaxLength(200)]
    [Required(ErrorMessage = "Required is a required field!")]
    public string Password { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public Role? Role { get; set; }
}
