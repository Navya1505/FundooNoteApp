using RepositoryModel.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryModel.Interface
{
    public  interface ILabelRL
    {
        public LabelEntity CreateLabel(long userId, long noteId, string LabelName);
    }
}
