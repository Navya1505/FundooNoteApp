using BusinessModel.Interface;
using CommonModel.Model;
using CommonModel.UserRegistrationModel;
using RepositoryModel.Entity;
using RepositoryModel.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
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
                userRegistrationModel.Password = Encrypt(userRegistrationModel.Password);
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
        private  string Encrypt(string clearText)
        {
            try
            {
                string encryptionKey = "MAKV2SPBNI99212";
                byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        clearText = Convert.ToBase64String(ms.ToArray());
                    }
                }
                return clearText;

            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public string Decrypt(string cipherText)
        {
            try
            {
                string encryptionKey = "MAKV2SPBNI99212";
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
                return cipherText;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}

