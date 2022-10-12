using RepositoryModel.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModel.Interface
{
    public  interface ILabelBL
    {
        public LabelEntity CreateLabel(long userId, long noteId, string LabelName);
        public List<LabelEntity> GetLabel(long userId);
    }
}
