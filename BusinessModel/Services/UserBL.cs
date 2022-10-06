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
        public string LoginUsers(string EmailID, string Password)
        {
            try
            {
                return userRL.LoginUsers(EmailID, Password);
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
        public ResetEntity ResetPasswordUser(string email, ResetPassword resetpassword)
        {
            try
            {
                return userRL.ResetPasswordUser(email,resetpassword);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}

