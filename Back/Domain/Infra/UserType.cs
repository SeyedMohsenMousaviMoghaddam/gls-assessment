using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DAL.Infra
{
	public enum UserType
	{
		[Display(Name = "Base")]
		Base = 0,
		[Display(Name = "Admin")]
		Admin = 1
	}
}