using CommonModel.Model;
using RepositoryModel.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModel.Interface
{
    public interface INoteBL
    {

        public NoteEntity CreateNoteUser(long UserId, Notes createnote);
        public List<NoteEntity> GetNotes(long userId);
        public bool UpdateNotes(long userId, long NoteId, Notes updateNote);
        public bool DeleteNotes(long userId, long NoteId);
        public bool PinNotes(long NoteId);


        public bool IsTrash(long userId, long NoteId);
        public bool Isarcheive(long userId, long NoteId);
        public NoteEntity UpdateNoteColor(long userId, long noteId, string color);

    }
}