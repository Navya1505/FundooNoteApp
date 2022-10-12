using CommonModel.Model;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RepositoryModel.Context;
using RepositoryModel.Entity;
using RepositoryModel.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryModel.Services
{
    public  class LabelRL:ILabelRL
    {
        private readonly FundooContext fundooContext;
        public LabelRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public LabelEntity CreateLabel(long userId, long noteId, string LabelName)
        {
            try
            {
                LabelEntity labelEntity = new LabelEntity();
                var result = fundooContext.NoteTable.Where(u => u.UserId == userId && u.NoteID == noteId).FirstOrDefault();
                
                if (result != null )
                {
                    labelEntity.LabelName = LabelName;
                    labelEntity.NoteID = noteId;
                    labelEntity.UserId = userId;
                    fundooContext.LabelTable.Add(labelEntity);
                    fundooContext.SaveChanges();
                    return labelEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<LabelEntity> GetLabel(long userId)
        {
            try
            {
                var result =fundooContext.LabelTable.Where(u=>u.UserId==userId).ToList();
                if (result != null)
                {
                    return result;
                }
                else
                    return  null;
                
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
