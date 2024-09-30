using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Identity
{
	public class SignUpViewModel
	{
		[Display(Name =	"First Name ")] // As Place Holder 
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;

        [Required ]
        public string UserName { get; set; } = null!;
		[EmailAddress]
        public string  Email { get; set; } = null!;
		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;
		[Display(Name ="Confirm Password ")]
		[DataType(DataType.Password)]
		[Compare("Password",ErrorMessage ="Pass Dosent Match ")]
		public string ConfirmPassword { get; set; } = null!;
		public bool IsAgree { get; set; }




    }
}
