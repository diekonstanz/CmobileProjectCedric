using MobileProjectJuist3.Models;

namespace MobileProjectJuist3.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public UserViewModel() { }

        public UserViewModel(User user)
        {
            Id = user.Id;
           

           
        }

    }
}
