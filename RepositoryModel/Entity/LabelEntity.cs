using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RepositoryModel.Entity
{
    public  class LabelEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LabelId { get; set; }
        public string LabelName { get; set; }
        [ForeignKey("NoteEntity")]
        public long NoteID{ get; set; }

        [ForeignKey("UserEntity")]
        public long UserId { get; set; }

    }
}
