using CommonModel.Model;
using RepositoryModel.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryModel.Interface
{
    public interface INoteRL
    {

        public NoteEntity CreatNoteUser(string email, Notes createnote);
        NoteEntity CreatNoteUser(string email, Notes createnote);
    }
}