using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TASK1_WPF.Models
{
    public class GroupUsers
    {
        public byte GroupUserID { get; set; }
        public string Name { get; set; }
        public DateTime NgayTao { get; set; }

        //lay ra danh sach cac user
        public virtual ICollection<User> Users { get; set; }
    }
}
