using CommonModel.Model;
using Microsoft.Extensions.Configuration;
using RepositoryModel.Context;
using RepositoryModel.Entity;
using RepositoryModel.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryModel.Services
{
    public  class NoteRL:INoteRL
    {
        private readonly FundooContext fundooContext;
       

        public NoteRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
            
        }


        public NoteEntity CreateNoteUser(long UserId, Notes createnote)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                var result = fundooContext.UserTableDB.Where(u => u.UserId == UserId).FirstOrDefault();
                noteEntity.Title = createnote.Title;
                noteEntity.Description = createnote.Description;
                noteEntity.Remainder = createnote.Remainder;
                noteEntity.Image = createnote.Image;
                noteEntity.Pin= createnote.Pin;
                noteEntity.Archieve = createnote.Archieve;
                noteEntity.Color = createnote.Color;
                noteEntity.Created = createnote.Created;
                noteEntity.Edited = createnote.Edited;
                noteEntity.Trash = createnote.Trash;
                noteEntity.UserId = result.UserId;
                fundooContext.NoteTable.Add(noteEntity);
                int ans = fundooContext.SaveChanges();
                if (ans > 0)
                    return noteEntity;
                else
                    return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}