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
        public string LoginUser(LoginModel loginModel);
        public string ForgetPassword(string EmailId);
        public ResetEntity ResetPasswordUser(string EmailId, ResetPassword resetpassword);
    }


}
