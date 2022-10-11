using CommonModel.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepositoryModel.Context;
using RepositoryModel.Entity;
using RepositoryModel.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RepositoryModel.Services
{
    public class NoteRL : INoteRL
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
                noteEntity.Pin = createnote.Pin;
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
        public List<NoteEntity> GetNotes(long userId)
        {
            try
            {
                var result = fundooContext.NoteTable.Where(u => u.UserId == userId).ToList();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool UpdateNotes(long userId, long NoteId, Notes updateNote)
        {
            try
            {
                NoteEntity updateNoteobj = new NoteEntity();
                var result = fundooContext.NoteTable.Where(u => u.UserId == userId && u.NoteID == NoteId).FirstOrDefault();
                if (result != null)
                {
                    //UpdateNoteEntity updateNoteobj = new UpdateNoteEntity();
                    result.Title = updateNote.Title;
                    result.Description = updateNote.Description;
                    result.Remainder = updateNote.Remainder;
                    result.Created = updateNote.Created;
                    result.Edited = updateNote.Edited;
                    result.Archieve = updateNote.Archieve;
                    result.Pin = updateNote.Pin;
                    result.Color = updateNote.Color;
                    result.Trash = updateNote.Trash;
                    fundooContext.NoteTable.Update(result);
                    int ans = fundooContext.SaveChanges();
                    if (ans > 0)
                        return true;
                    else
                        return false;


                    updateNoteobj.Title = updateNote.Title;
                    updateNoteobj.Description = updateNote.Description;
                    updateNoteobj.Remainder = updateNote.Remainder;
                    updateNoteobj.Color = updateNote.Color;
                    updateNoteobj.Image = updateNote.Image;
                    updateNoteobj.Archieve = updateNote.Archieve;
                    updateNote.Pin = updateNote.Pin;
                    updateNoteobj.Trash = updateNote.Trash;
                    updateNoteobj.Created = updateNote.Created;
                    updateNoteobj.Edited = updateNote.Edited;
                    updateNoteobj.NoteID = result.NoteID;
                    updateNoteobj.UserId = result.UserId;
                    fundooContext.NoteTable.Update(updateNoteobj);
                    int x = fundooContext.SaveChanges();

                    if (x > 0)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public bool DeleteNotes(long userId, long NoteId)
        {
            try
            {
                var result = fundooContext.NoteTable.Where(u => u.UserId == userId && u.NoteID == NoteId).FirstOrDefault();
                if (result != null)
                {
                    fundooContext.NoteTable.Remove(result);
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                    return false;

            }
            catch (Exception ex)
            {
                throw ex; ;
            }
        }
        public bool PinNotes(long NoteId)
        {
            try
            {
                var result = fundooContext.NoteTable.Where(u => u.NoteID == NoteId).FirstOrDefault();
                if (result != null && result.Pin == true)
                {
                    result.Pin = false;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {

                    return false;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool IsTrash(long userId, long NoteId)
        {
            try
            {
                var result = fundooContext.NoteTable.Where(u => u.UserId == userId && u.NoteID == NoteId).FirstOrDefault();
                if (result != null && result.Trash == true)
                {
                    result.Trash = false;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {

                    return false;

                }

            }
            catch (Exception e)
            {
                throw e;

            }

        }


        public bool Isarcheive(long userId, long NoteId)
        {
            try
            {
                var result = fundooContext.NoteTable.Where(u => u.UserId == userId && u.NoteID == NoteId).FirstOrDefault();

                if (result != null && result.Archieve == true)
                {
                    result.Archieve = false;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {

                    return false;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public NoteEntity UpdateNoteColor(long userId, long noteId, string color)
        {
            try
            {
                var result = fundooContext.NoteTable.Where(u => u.UserId == userId && u.NoteID == noteId).FirstOrDefault();
                if (result != null)
                {
                    result.Color = color;
                    fundooContext.NoteTable.Update(result);
                    fundooContext.SaveChanges();
                    return result;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                throw e;
            }


        }
        public NoteEntity Image(long userId, long noteId, IFormFile file)
        {
            try
            {
                var result = fundooContext.NoteTable.Where(u => u.UserId == userId && u.NoteID == noteId).FirstOrDefault();
                if (result != null)
                {
                    CloudinaryDotNet.Account account = new CloudinaryDotNet.Account { ApiKey = "282598545727559", ApiSecret = "XimNVxmqTLuRuA1Fgg_Gb3R0cH0", Cloud = "dlojqt0x9" };
                    Cloudinary _cloudinary = new Cloudinary(account);
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, file.OpenReadStream())
                    };
                    var uploadresult = _cloudinary.Upload(uploadParams);
                    result.Image = uploadresult.Url.ToString();
                    fundooContext.SaveChanges();
                    return result;

                }
                else
                    return null;
            }
            catch(Exception e)
            {
                throw e;
            }
            }
      
    }
    }
