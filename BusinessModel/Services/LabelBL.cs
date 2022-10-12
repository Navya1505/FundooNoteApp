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
        public LabelBL(ILabelRL LabelRL)
        {

            this.labelRL = labelRL;
        }


        public LabelEntity CreateLabel(long userId, long noteId, string LabelName)
        {
            try
            {
                return labelRL.CreateLabel(userId, noteId, LabelName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}