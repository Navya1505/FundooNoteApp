using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;
using System.Text;

namespace CommonModel.Model
{
    public class ImageUpload
    {
        [Key]
        [Column(TypeName="bigint")]
        public long Id { get; set; }
        [Column(TypeName = "varChar(max)")]
        public string imagepath { get; set; }
        [Column(TypeName = "Datetime")]
        public DateTime InsertedOn { get; set; }
        public IFormFile ProfileImage { get; set; }
    }
}
