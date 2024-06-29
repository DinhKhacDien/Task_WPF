using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TASK1_WPF.Models
{
    [Table("User")]
    public class User
    {
        public Guid UserID { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string?Address { get; set; }
        public DateTime NgayTao { get; set; }
        public byte GroupUserID { get; set; }
        public GroupUsers GroupUser { get; set; }

    }
}
