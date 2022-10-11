using RepositoryModel.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryModel.Interface
{
    public interface  ICollabRL
    {
        public CollaborateEntity AddCollaborate(long userId, long NoteId, string Receiver_Email);
        public List<CollaborateEntity> GetCollaborate(long userId);
        public bool RemoveCollaborate(long noteId, long userId, string emailId);



        }
}