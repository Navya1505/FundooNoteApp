using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RepositoryModel.Entity
{
    public class NoteEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NoteID { get; set; }
        public string Title { get; set; }
        public string Description{get; set; }
        public DateTime Remainder { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public bool Archieve { get; set; }
        public bool Pin { get; set; }

        public bool Trash { get; set; }
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }


        [ForeignKey("User")]
        
        public long UserId { get; set; }
        
    }
}
