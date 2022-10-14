using BusinessModel.Interface;
using BusinessModel.Services;
using CommonModel.Model;
using CommonModel.UserRegistrationModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<UserController> _logger;
        public UserController(IUserBL userBL, ILogger<UserController> _logger)
        {
            this.userBL = userBL;
            this._logger = _logger;
        }
        [HttpPost("Register")]
        public IActionResult Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                var result = userBL.Registration(userRegistrationModel);
                if (result != null)

                {
                    _logger.LogInformation("User Registration Successfull from  Register route");
                    return this.Ok(new { sucess = true, message = "UserRegistration sucessfull", data = result });
                }
                else
                {
                    _logger.LogInformation("User Registration UnSuccessfull from Register route");
                    return this.BadRequest(new { success = false, message = "UserRegistration fail" });
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
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
                    _logger.LogInformation(" LoginSuccessfull from  Login route");
                    return this.Ok(new { success = true, message = $"Login Successfull", data = userdata });
                }
                else
                {
                    _logger.LogInformation("Login UnSuccessfull from  Login route");
                    return this.NotFound(new { success = false, message = $"Login UnSuccessfull " });
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
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
                    _logger.LogInformation("Password reset link send Sucressfull from Forgetpassword route");
                    return this.Ok(new { success = true, message = "Password reset link send Successfull" });
                }
                else
                {
                    _logger.LogInformation(" User not Registered from  ForgetPassword route");
                    return this.BadRequest(new { success = false, message = "User not registered" });
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
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
                    _logger.LogInformation(" Resetpassword Sucessfull from  ResetPassword route");
                    return this.Ok(new { success = true, message = "Reset password Successfull", data = userdata });
                }
                else
                {
                    _logger.LogInformation(" Resetpassword UnSucessfull from  ResetPassword route");
                    return this.BadRequest(new { success = false, message = "Reset Password UnSucessfull" });
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }
        }


    }
}









