using CommonModel.Model;
using RepositoryModel.Context;
using RepositoryModel.Entity;
using RepositoryModel.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryModel.Services
{
    public  class CollabRL:ICollabRL
    {
        private readonly FundooContext fundooContext;
        public CollabRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public CollaborateEntity AddCollaborate(long userId, long NoteId, string Receiver_Email)
        {
            try
            {
               CollaborateEntity collaborateEntity = new CollaborateEntity();
                var result1 = fundooContext.NoteTable.Where(u => u.UserId == userId && u.NoteID == NoteId).FirstOrDefault();
                var result2 = fundooContext.UserTableDB.Where(u => u.EmailID == Receiver_Email).FirstOrDefault();
                if (result1 != null && result2 != null)
                {
                    collaborateEntity.Sender_UserId = userId;
                    collaborateEntity.NoteId = NoteId;
                    collaborateEntity.Receiver_Email = Receiver_Email;
                    collaborateEntity.Receiver_UserId = result2.UserId;
                    fundooContext.CollaborateTableDB.Add(collaborateEntity);
                    fundooContext.SaveChanges();
                    return collaborateEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<CollaborateEntity> GetCollaborate(long userId)
        {
            try
            {
                var result1 = fundooContext.CollaborateTableDB.Where(u => u.Sender_UserId == userId).ToList();
                return result1;

            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public bool RemoveCollaborate(long noteId, long userId, string emailId)
        {
            try
            {
                var result=fundooContext.CollaborateTableDB.Where(e=>e.NoteId == noteId && e.Sender_UserId == userId && e.Receiver_Email == emailId).First();
                if (result != null)
                {
                    fundooContext.CollaborateTableDB.Remove(result);
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                return false;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        }
        }
    

