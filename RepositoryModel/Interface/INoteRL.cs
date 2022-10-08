using CommonModel.Model;
using RepositoryModel.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryModel.Interface
{
    public interface INoteRL
    {

        public NoteEntity CreateNoteUser(long UserId,Notes createnote);
       
    }
}