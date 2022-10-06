using System;
using System.Collections.Generic;
using System.Text;

namespace CommonModel.Model
{
    public  class Notes
    {
       
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Remainder { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public bool Archieve { get; set; }
        public bool Pin { get; set; }

        public bool Trash { get; set; }
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }
    }
}
