using Microsoft.EntityFrameworkCore;
using RepositoryModel.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryModel.Context
{
    public class FundooContext:DbContext
    {
        public FundooContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<UserEntity> UserTableDB { get; set; }
        public DbSet<NoteEntity> NoteTable { get; set; }
        public DbSet<CollaborateEntity> CollaborateTableDB { get; set; }
        public DbSet<LabelEntity> LabelTable { get; set; }
    }
}

