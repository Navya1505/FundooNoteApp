using CommonModel.Model;
using RepositoryModel.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryModel.Interface
{
    public interface INoteRL
    {

        public NoteEntity CreateNoteUser(long UserId,Notes createnote);
        public List<NoteEntity> GetNotes(long userId);
        public bool UpdateNotes(long userId, long NoteId, Notes updateNote);
        public bool DeleteNotes(long userId, long NoteId);
        public bool PinNotes(long userId, long NoteId);


    }
}