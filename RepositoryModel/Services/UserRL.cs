﻿using CommonModel.Model;
using CommonModel.UserRegistrationModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryModel.Context;
using RepositoryModel.Entity;
using RepositoryModel.Interface;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryModel.Services
{
    public class UserRL : IUserRL
    {
        private readonly FundooContext fundooContext;
        public IConfiguration configuration { get; }
        public UserRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }
        public UserEntity Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = userRegistrationModel.FirstName;
                userEntity.LastName = userRegistrationModel.LastName;
                userEntity.EmailID = userRegistrationModel.EmailID;
                userEntity.Password = userRegistrationModel.Password;
                fundooContext.UserTableDB.Add(userEntity);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string LoginUsers(string EmailId, string Password)
        {
            try
            {
                var result = fundooContext.UserTableDB.Where(u => u.EmailID == EmailId && u.Password == Password).FirstOrDefault();
                if (result != null)
                {
                    return GetJWTToken(EmailId, result.UserId);
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private static string GetJWTToken(string EmailId, int userID)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("EmailId", EmailId),
                    new Claim("userID",userID.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),

                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string ForgetPassword(string email)
        {
            try
            {
                var emailcheck = fundooContext.UserTableDB.FirstOrDefault(e => e.EmailID == email);
                if (emailcheck != null)
                {
                    var token = GetJWTToken(emailcheck.EmailID, emailcheck.UserId);
                    MSMQModel mSMQ = new MSMQModel();
                    mSMQ.sendData2Queue(token);
                    return token.ToString();
                }
                else
                    return null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ResetEntity ResetPasswordUser(string email,ResetPassword resetpassword) 
        { 
            try
            {
                ResetEntity resetentity = new ResetEntity();
                 resetentity.NewPassword = resetpassword.NewPassword;
                resetentity.ConfirmPassword = resetpassword.ConfirmPassword;
                var result = fundooContext.UserTableDB.Where(u => u.EmailID == email).FirstOrDefault();
                if (result != null)
                {
                    result.Password = resetpassword.NewPassword;
                    int ans = fundooContext.SaveChanges();
                    if (ans > 0)
                        return resetentity;
                    else
                        return null;
                    //fundooContext.UserTable.Update()
                }
                else
                    return null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       
    }
}