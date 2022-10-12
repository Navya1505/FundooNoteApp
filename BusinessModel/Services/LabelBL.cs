using BusinessModel.Interface;
using Microsoft.Data.SqlClient.DataClassification;
using RepositoryModel.Entity;
using RepositoryModel.Interface;
using RepositoryModel.Services;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace BusinessModel.Services
{
    public class LabelBL : ILabelBL
    {
        private readonly ILabelRL labelRL;
        public LabelBL(ILabelRL labelRL)
        {

            this.labelRL = labelRL;
        }


        public LabelEntity CreateLabel(long userId, long NoteId, string LabelName)
        {
            try
            {
                return labelRL.CreateLabel(userId, NoteId, LabelName);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List <LabelEntity>GetLabel(long userId)
        {
            try
            {
                return labelRL.GetLabel(userId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }

}