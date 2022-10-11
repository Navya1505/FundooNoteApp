using BusinessModel.Interface;
using CommonModel.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration.UserSecrets;
using RepositoryModel.Entity;
using RepositoryModel.Interface;
using RepositoryModel.Services;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
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
        public bool PinNotes(long NoteId)
        {
            try
            {
                return noteRL.PinNotes(NoteId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public bool IsTrash(long userId, long NoteId)
        {
            try
            {
                return noteRL.IsTrash(userId, NoteId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public bool Isarcheive(long userId, long NoteId)
        {
            try
            {
                return noteRL.Isarcheive(userId, NoteId);
            }

            catch(Exception e)
            {
                throw e;
            }
        }

        public NoteEntity UpdateNoteColor(long userId, long noteId, string color)
        {
            try
            {
                return noteRL.UpdateNoteColor(userId, noteId, color);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public NoteEntity Image(long userId,long noteId, IFormFile file)
        {
            try
            {
                return noteRL.Image(userId, noteId, file);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

    }
}