global using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
namespace IKEA.PL.ViewModels.AccountViewModels
{
    public class SignInViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    

}
}
