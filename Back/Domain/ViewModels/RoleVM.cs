using DAL.Infra;

namespace DAL.ViewModels
{
    public class RoleVM
    {
        public int Id { get; set; }

        public UserType Name { get; set; }
        public string Description { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public string UserCreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public int ModifiedBy { get; set; }

        public string UserModifiedBy { get; set; }

        public bool StateCode { get; set; }
    }
}
