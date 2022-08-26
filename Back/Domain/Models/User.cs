using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DAL.Models
{
    [Table("User")]
    public class User
    {
        public User()
        {
            this.Roles = new HashSet<Role>();
            this.UserLoginLogs = new HashSet<UserLoginLog>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
        //[RegularExpression(pattern: @"[0|۰]{1}[9|۹]{1}[۰|0-۹|9]{9}", ErrorMessage = "is a valid MobileNumber")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        public string MobileNumber { get; set; }
        //[RegularExpression(pattern: @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "is a valid Email address")]
        //public string Email { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public int ModifiedBy { get; set; }

        public bool StateCode { get; set; }

        public DateTime? LASTLOGIN { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<UserLoginLog> UserLoginLogs { get; set; }
    }
}