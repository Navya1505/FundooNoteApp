using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Text;

namespace CommonModel.Model
{
    public class ImageUpload
    {
        [Key]
        [Column(TypeName="")]
        public long Id { get; set; }
        public string imagepath { get; set; }
        public DateTime InsertedOn { get; set; }
    }
}
