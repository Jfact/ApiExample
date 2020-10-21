using System.ComponentModel.DataAnnotations;

namespace EntitiesLibrary.Entities.DataTransfer
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
    public class UserCreateDto
    {
        [Required(ErrorMessage = "User email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "User phone number is required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "User password is required")]
        public string Password { get; set; }
    }
    public class UserEditDto
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        [Required(ErrorMessage = "Current password for validation is required")]
        public string PasswordForValidation { get; set; }
    }
    public class UserLoginDto
    {
        [Required(ErrorMessage = "User email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "User password is required")]
        public string Password { get; set; }
    }
}
