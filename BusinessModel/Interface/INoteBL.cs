using CommonModel.Model;
using RepositoryModel.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModel.Interface
{
    public interface INoteBL
    {

        public NoteEntity CreatNoteUser(string email, Notes createnote);

    }
}