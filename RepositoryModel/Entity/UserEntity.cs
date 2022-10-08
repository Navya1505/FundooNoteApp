using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RepositoryModel.Entity
{
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string EmailID { get; set; }
        public string Password { get; set; }
    }
    public class LoginEntity
    {
        public string EmailID { get; set; }
        public string Password { get; set; }
    }
        public class ResetEntity
        {
            public string EmailID{ get; set; }
            public string NewPassword { get; set; }
            public string ConfirmPassword { get; set; }
        }

    }