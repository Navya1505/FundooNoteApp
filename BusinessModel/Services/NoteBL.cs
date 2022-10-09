using BusinessModel.Interface;
using CommonModel.Model;
using Microsoft.Extensions.Configuration.UserSecrets;
using RepositoryModel.Entity;
using RepositoryModel.Interface;
using RepositoryModel.Services;
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
        public List<NoteEntity> GetNotes(long userId)
        {
            try
            {
                return noteRL.GetNotes(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool UpdateNotes(long userId, long NoteId, Notes updateNote)
        {
            try
            {
                return noteRL.UpdateNotes(userId, NoteId, updateNote);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool DeleteNotes(long userId, long NoteId)
        {
            try
            {
                return noteRL.DeleteNotes(userId, NoteId);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public bool PinNotes(long userId,long NoteId)
        {
            try
            {
                return noteRL.PinNotes(userId, NoteId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}