namespace Kimlik.Models.ViewModels
{
    public class ProfilePasswordMViewModel
    {
        public ProfileViewModel ProfileViewModel { get; set; } = new ProfileViewModel();
        public ChangePasswordViewModel ChangePasswordViewModel { get; set; } = new ChangePasswordViewModel();
    }
}
