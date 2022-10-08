using BusinessModel.Interface;
using BusinessModel.Services;
using CommonModel.Model;
using CommonModel.UserRegistrationModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryModel.Entity;
using System;
using System.Security.Claims;

namespace FundooNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        [HttpPost("Register")]
        public IActionResult Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                var result = userBL.Registration(userRegistrationModel);
                if (result != null)
                {
                    return this.Ok(new { sucess = true, message = "UserRegistration sucessfull", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "UserRegistration fail" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("login")]
        public IActionResult LoginUser(LoginModel loginModel)
        {
            try
            {
                var userdata = userBL.LoginUser(loginModel);
                if (userdata != null)
                {
                    return this.Ok(new { success = true, message = $"Login Successfull" , data=userdata });
                }
                else
                return this.NotFound(new { success = false, message = $"Login UnSuccessfull " });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("ForgetPassword")]

        public IActionResult ForgetPassword(string EmailId)
        {
            try
            {
                var result = userBL.ForgetPassword(EmailId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Password reset link send Successfull" });
                }
                else
                    return this.BadRequest(new { success = false, message = "User not registered" });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize]
        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(ResetPassword resetpassword)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var userdata = userBL.ResetPasswordUser(email, resetpassword);

                if (userdata != null)
                {
                    return this.Ok(new { success = true, message = "Reset password Successfull", data = userdata });
                }
                else
                    return this.BadRequest(new { success = false, message = "Reset Password failed" });

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}









