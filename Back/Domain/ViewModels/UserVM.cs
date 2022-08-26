using DAL.Models;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DAL.ViewModels
{
    public class UserVM
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        //[RegularExpression(pattern: @"[0|۰]{1}[9|۹]{1}[۰|0-۹|9]{9}", ErrorMessage = "is a valid MobileNumber")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        public string MobileNumber { get; set; }
        //[RegularExpression(pattern: @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "is a valid Email address")]
        //public string Email { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public string UserCreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public int ModifiedBy { get; set; }

        public string UserModifiedBy { get; set; }

        public bool StateCode { get; set; }
        public DateTime? LASTLOGIN { get; set; }
        public List<RoleVM> Roles { get; set; }
        public List<UserLoginLog> UserLoginLogs { get; set; }
    }
    public class UserValidator : AbstractValidator<UserVM>
    {
        public UserValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).Length(0, 10);
            RuleFor(x => x.UserName).EmailAddress();
            //RuleFor(x => x.StateCode).InclusiveBetween(0, 2);
            RuleFor(x => x.Password).Equal(x => x.ConfirmPassword).WithMessage("Your password and confirmation password do not match.");

        }
    }
}