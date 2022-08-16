using DAL.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("Role")]
    public class Role
    {
        public Role()
        {
            this.Users = new HashSet<User>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public UserType Name { get; set; }
        public string Description { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public int ModifiedBy { get; set; }

        public bool StateCode { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
