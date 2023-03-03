using ULDeneme.BLL.Concrete.ResultServiceBLL;
using ULDeneme.Model.Entities;
using ULDeneme.ViewModel.UserViewModels;

namespace ULDeneme.BLL.Abstract
{
    public interface IUserBLL
    {
        ResultService<bool> CheckUserForLogin(string email, string password);
        ResultService<User> GetByEMail(string email);
        ResultService<UserCreateVM> Insert(UserCreateVM user);
        ResultService<bool> Update(User user);
        ResultService<bool> Delete(User user);
    }
}
