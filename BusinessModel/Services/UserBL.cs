using BusinessModel.Interface;
using CommonModel.Model;
using CommonModel.UserRegistrationModel;
using RepositoryModel.Entity;
using RepositoryModel.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModel.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }



        public UserEntity Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                return userRL.Registration(userRegistrationModel);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public string  LoginUser(LoginModel loginModel)
        {
            try
            {
                return userRL.LoginUser(loginModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string ForgetPassword(string EmailID)
        {
            try
            {
                return userRL.ForgetPassword(EmailID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ResetEntity ResetPasswordUser(string EmailID, ResetPassword resetpassword)
        {
            try
            {
                return userRL.ResetPasswordUser(EmailID,resetpassword);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}

