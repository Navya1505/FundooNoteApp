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
        public NoteEntity CreateNoteUser(long userId, Notes createnote)
        {
            try
            {
                return noteRL.CreateNoteUser(userId, createnote);
            }
            catch (Exception ex)        
            {
                throw ex;
            }
        }
     }
    }
