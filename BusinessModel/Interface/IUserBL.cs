using CommonModel.Model;
using CommonModel.UserRegistrationModel;
using RepositoryModel.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModel.Interface
{
    public interface IUserBL
    {
        public UserEntity Registration(UserRegistrationModel userRegistrationModel);
        public string LoginUsers(string EmailId, string Password);
        public string ForgetPassword(string EmailId);
        public ResetEntity ResetPasswordUser(string email, ResetPassword resetpassword);
    }


}
