using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ULDeneme.BLL.Abstract;
using ULDeneme.BLL.Concrete.ResultServiceBLL;
using ULDeneme.DAL.Abstract;
using ULDeneme.Model.Entities;
using ULDeneme.Model.Enums;
using ULDeneme.ViewModel.UserViewModels;
using UserRole = ULDeneme.Model.Enums.UserRole;

namespace ULDeneme.BLL.Concrete
{
    public class UserService : IUserBLL
    {
        IUserDAL _userRepository;

        public UserService(IUserDAL userRepository)
        {
            this._userRepository = userRepository;
        }

        public ResultService<bool> CheckUserForLogin(string email, string password)
        {
            ResultService<bool> result = new ResultService<bool>();
            User user = _userRepository.Get(a => a.Email == email && a.Password == password && a.IsActive);

            if (user == null)
            {
                result.AddError("Login Hatası", "Login işlemi başarısız.");
                return result;
            }

            result.Data = true;
            return result;
        }

        private void CheckPassword(string password)
        {
            if (password.Length <= 3)
            {
                throw new Exception("Password minimum 3 chracter");
            }
        }

        public ResultService<User> GetByEMail(string email)
        {
            ResultService<User> result = new ResultService<User>();
            try
            {
                User user = _userRepository.Get(a => a.Email == email);
                if (user == null)
                {
                    result.AddError("user not found", "user not found");
                    return result;
                }
                result.Data = user;
                return result;
            }
            catch (Exception ex)
            {
                result.AddError("exception", ex.Message);
                return result;
            }
        }

        public ResultService<UserCreateVM> Insert(UserCreateVM user)
        {
            ResultService<UserCreateVM> userResult = new ResultService<UserCreateVM>();

            try
            {
                CheckPassword(user.Password);
                User addedUser = _userRepository.Add(new User
                {
                    NickName = user.NickName,
                    Email = user.Email,
                    Password = user.Password,
                    UserRole = UserRole.Standart,

                });
            }
            catch (Exception ex)
            {

                userResult.AddError("Exception", ex.Message);
            }

            return userResult;
        }

        public ResultService<bool> Update(User user)
        {
            ResultService<bool> result = new ResultService<bool>();
            try
            {
                User updatedUser = _userRepository.Update(user);
                if (updatedUser == null)
                {
                    result.AddError("Update Error", "Update is notsuccessful.");
                    return result;
                }
                result.Data = true;
                return result;
            }
            catch (Exception ex)
            {
                result.AddError("exception", ex.Message);
                return result;
            }
        }
        public ResultService<bool> Delete(User user)
        {
            ResultService<bool> result = new ResultService<bool>();
            try
            {
                _userRepository.Remove(user);
                result.Data = true;
                return result;
            }
            catch (Exception ex)
            {
                result.AddError("exception", ex.Message);
                return result;
            }
        }

    }

}

