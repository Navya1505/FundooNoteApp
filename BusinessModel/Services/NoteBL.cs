using BusinessModel.Interface;
using CommonModel.Model;
using RepositoryModel.Entity;
using RepositoryModel.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModel.Services
{
    public class NoteBL : INoteBL
    {
       
            private readonly INoteRL noteRL;
            public NoteBL(INoteRL noteRL)
            {
                this.noteRL = noteRL;
            }

        public NoteEntity CreateNoteUser(string email,Notes createnote)
        {
            try
            {
                return noteRL.CreatNoteUser(email, createnote);
            }
            catch (Exception ex)        
            {
                throw ex;
            }
        }

        public NoteEntity CreatNoteUser(string email, Notes createnote)
        {
            throw new NotImplementedException();
        }
    }
    }
