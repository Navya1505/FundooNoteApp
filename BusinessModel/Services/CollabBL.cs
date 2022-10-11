using BusinessModel.Interface;
using RepositoryModel.Entity;
using RepositoryModel.Interface;
using RepositoryModel.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModel.Services
{
    public class CollabBL : ICollabBL
    {

        private readonly ICollabRL collabRL;
        public CollabBL(ICollabRL collabRL)
        {
            this.collabRL = collabRL;
        }

        public CollaborateEntity AddCollaborate(long userId, long NoteId, string Receiver_Email)

        {
            try
            {
                return collabRL.AddCollaborate(userId, NoteId, Receiver_Email);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CollaborateEntity> GetCollaborate(long userId)
        {
            try
            {
                return collabRL.GetCollaborate(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public bool RemoveCollaborate(long noteId, long userId, string emailId)
        {
            try
            {
                return collabRL.RemoveCollaborate(noteId, userId, emailId);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}